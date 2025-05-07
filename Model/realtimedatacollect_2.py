import serial
from scipy.stats import entropy
import csv
import time
import numpy as np
import joblib
import pandas as pd
import socket

# Set the IP and port to send data
UDP_IP = "127.0.0.1"  # Localhost (change this if running on different machines)
UDP_PORT = 5052  # Must match Unity's receiving port


# Create a UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

# Serial port settings (for COM5)
serial_port = 'COM3'  # Change to your correct COM port
baud_rate = 115200
timeout = 1  # Adjust timeout as necessary

# Window settings
window_size = 1  # 1 second
sampling_rate = 60  # Assumed sampling rate (samples per second)
window_size_samples = int(window_size * sampling_rate)  # 60 samples per window
overlap = 0.50  # 50% overlap
overlap_samples = int(overlap * window_size_samples)  # 50% overlap

ser = serial.Serial(serial_port, baud_rate, timeout=timeout)

with open('featuredata.csv', mode='a', newline='') as file:
    writer = csv.writer(file)
    # Write header with 36 columns: timestamp + 36 sensor values
    yn=input("Do you want to print the columns?y/n:")
    if yn=='y':
        writer.writerow(
                    # [f"Sensor{i}{axis}{n}" for n in range(20) for i in range(3) for axis in ["accelX", "accelY", "accelZ", "gyroX", "gyroY", "gyroZ"]]+
                    [f"s0ax{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s0ay{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s0az{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+
                    [f"s0gx{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s0gy{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s0gz{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+
                    [f"s1ax{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s1ay{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s1az{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+
                    [f"s1gx{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s1gy{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s1gz{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+
                    [f"s2ax{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s2ay{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s2az{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+
                    [f"s2gx{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s2gy{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+[f"s2gz{axis}" for axis in ["mean","std","median","peak-to-peak","IQR","rms","spectral_centroid","spectral_entropy"]]+
                    ['senso0azmax']+['senso0azmin'])
    data_buffer = []  # Buffer to hold incoming data
    sensor_count = 0

    s0ax, s0ay, s0az, s0gx, s0gy, s0gz = [], [], [], [], [], []
    s1ax, s1ay, s1az, s1gx, s1gy, s1gz = [], [], [], [], [], []
    s2ax, s2ay, s2az, s2gx, s2gy, s2gz = [], [], [], [], [], []


    while True:
        # Read a line of data from the serial port
        line = ser.readline().decode('utf-8').strip()

        # Debugging: Print the line read from serial
        print(f"Received: {line}")

        
        
        if line:  # If the line is not empty
            try:
                # Split the string at the colon ":" and handle only the part after the colon
                parts = line.split(':')
                
                if len(parts) < 2:
                    print("Error: Incorrect data format. Skipping line.")
                    continue

                sensor_data = parts[1].strip().split(',')
                sensor_index=parts[0]
                print(sensor_index)


                # Make sure there are 6 data points per sensor (3 accelerometer + 3 gyroscope)
                if len(sensor_data) != 6:
                    print(f"Error: Invalid number of data points. Expected 6, got {len(sensor_data)}. Skipping line.")
                    continue

                sensor_data = [float(x) for x in sensor_data]  # Convert all values to floats

                x_minA=-39.2
                x_maxA=39.2
                x_minG=-500
                x_maxG=500

                def accelnorm(x):
                    x_norm=(2*(x-x_minA)/(x_maxA-x_minA))-1
                    return x_norm

                def gyronorm(x):
                    x_norm=(2*(x-x_minG)/(x_maxG-x_minG))-1
                    return x_norm
                

                sensor_data[0]=accelnorm(sensor_data[0]) 
                sensor_data[1]=accelnorm(sensor_data[1])
                sensor_data[2]=accelnorm(sensor_data[2])
                sensor_data[3]=gyronorm(sensor_data[3])
                sensor_data[4]=gyronorm(sensor_data[4])
                sensor_data[5]=gyronorm(sensor_data[5]) 


                
                if sensor_index=="sensor 0":
                    s0ax.append(sensor_data[0])
                    s0ay.append(sensor_data[1])
                    s0az.append(sensor_data[2])
                    s0gx.append(sensor_data[3])
                    s0gy.append(sensor_data[4])
                    s0gz.append(sensor_data[5])

                    print("sensor 0 done")
                elif sensor_index=="sensor 1":
                    s1ax.append(sensor_data[0])
                    s1ay.append(sensor_data[1])
                    s1az.append(sensor_data[2])
                    s1gx.append(sensor_data[3])
                    s1gy.append(sensor_data[4])
                    s1gz.append(sensor_data[5])

                   
                    print("sensor 1 done")
                elif sensor_index=="sensor 2":
                    s2ax.append(sensor_data[0])
                    s2ay.append(sensor_data[1])
                    s2az.append(sensor_data[2])
                    s2gx.append(sensor_data[3])
                    s2gy.append(sensor_data[4])
                    s2gz.append(sensor_data[5])

                    print("sensor 2 done")
                
                # Add the data to the buffer
                timestamp = time.time()  # Get the current timestamp
                data_buffer.append((timestamp, sensor_data))

            except Exception as e:
                print(f"Error parsing data: {e}")
                continue
        
        # Process windows with 50% overlap when enough data is collected
        if len(data_buffer) >= window_size_samples:
                window_data = data_buffer[:window_size_samples]
                window_timestamp = window_data[0][0]  # Use the timestamp of the first data point in the window

                # Flatten the window data (timestamp + 36 sensor values)
                flattened_data = []
                for sensor_data in window_data:  
                    flattened_data.extend(sensor_data[1])  # Add sensor data to the row

                def calculate_features(data,fs=20):

                    data = np.array(data)

                    # Apply FFT
                    fft_result = np.fft.fft(data)
                    freqs = np.fft.fftfreq(len(data), d=1/fs)
                    magnitudes = np.abs(fft_result)
                    
                    # Filter only positive frequencies
                    positive_freqs = freqs[freqs >= 0]
                    positive_magnitudes = magnitudes[freqs >= 0]

                    
                    spectral_centroid = np.sum(positive_freqs * positive_magnitudes) / np.sum(positive_magnitudes)  # Weighted avg
                    
                    hist, bin_edges = np.histogram(data, bins=5, density=True)
                    hist = hist / np.sum(hist)
                    entropy_value = entropy(hist)
                    return [np.round(np.mean(data), 8),
                            np.round(np.std(data), 8),
                             np.round(np.median(data), 8),
                             np.round(np.max(data) - np.min(data), 8),
                             np.round(np.percentile(data, 75) - np.percentile(data, 25), 8),  # Interquartile range
                             np.round(np.sqrt(np.mean(np.square(data))), 8),  # RMS
                             
                             np.round(spectral_centroid, 8),
                             np.round(entropy_value, 8)

                             ]  
                    
                # Features for Sensor 0
                s0ax_features = calculate_features(s0ax)
                s0ay_features = calculate_features(s0ay)
                s0az_features = calculate_features(s0az)
                s0gx_features = calculate_features(s0gx)
                s0gy_features = calculate_features(s0gy)
                s0gz_features = calculate_features(s0gz)

                # Features for Sensor 1
                s1ax_features = calculate_features(s1ax)
                s1ay_features = calculate_features(s1ay)
                s1az_features = calculate_features(s1az)
                s1gx_features = calculate_features(s1gx)
                s1gy_features = calculate_features(s1gy)
                s1gz_features = calculate_features(s1gz)

                # Features for Sensor 2
                s2ax_features = calculate_features(s2ax)
                s2ay_features = calculate_features(s2ay)
                s2az_features = calculate_features(s2az)
                s2gx_features = calculate_features(s2gx)
                s2gy_features = calculate_features(s2gy)
                s2gz_features = calculate_features(s2gz)
                

                senso0azmax=np.round(np.max(np.array(s0az)),8)                
                senso0azmin=np.round(np.min(np.array(s0az)),8)               

                # Write the window data to the CSV file
            
                            
                writer.writerow(
                                
                                 s0ax_features + s0ay_features + s0az_features + s0gx_features + s0gy_features +s0gz_features+
                                 s1ax_features + s1ay_features + s1az_features + s1gx_features + s1gy_features + s1gz_features+
                                 s2ax_features + s2ay_features + s2az_features + s2gx_features + s2gy_features + s2gz_features+
                                [senso0azmax]+[senso0azmin]
                                
                                ) 


                model = joblib.load("svm_model.pkl")
                #i want to take the lastrow of the featuredata.csv file here for predicting

                file.flush()  # This ensures data is written to disk immediately
                
                df = pd.read_csv('featuredata.csv')
                
                if not df.empty:
                     last_row =df.iloc[-1].values
                     last_row = np.array(last_row).reshape(1,-1)
                     prediction = model.predict(last_row)[0]
                     action = str(prediction).encode()
                     sock.sendto(action, (UDP_IP, UDP_PORT))


                    #  sock.sendto(prediction.encode(),UDP_PORT)
                     print(f"real-time prediction: {prediction}")
                     action=str(prediction).encode()
                     sock.sendto(action, (UDP_IP, UDP_PORT))
                     print(f"Sent: {prediction}")
                             

                # Remove `overlap_samples` from the buffer to prepare for the next window
                data_buffer = data_buffer[overlap_samples:]
                s0ax=s0ax[10:]
                s0ay=s0ay[10:]
                s0az=s0az[10:]
                s0gx=s0gx[10:]
                s0gy=s0gy[10:]
                s0gz=s0gz[10:]

                s1ax=s1ax[10:]
                s1ay=s1ay[10:]
                s1az=s1az[10:]
                s1gx=s1gx[10:]
                s1gy=s1gy[10:]
                s1gz=s1gz[10:]

                s2ax=s2ax[10:]
                s2ay=s2ay[10:]
                s2az=s2az[10:]
                s2gx=s2gx[10:]
                s2gy=s2gy[10:]
                s2gz=s2gz[10:]        



        # Sleep to simulate the data sampling rate (optional, based on actual sampling rate)
        
        time.sleep(1/ sampling_rate)

     

