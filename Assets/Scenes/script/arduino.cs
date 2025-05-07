using System;
using System.IO.Ports;
using UnityEngine;

public class SerialReader : MonoBehaviour
{
    SerialPort serialPort;
    public string portName = "COM3"; // Change this based on your system
    public int baudRate = 115200;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
        serialPort.ReadTimeout = 50; // Adjust timeout as needed
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();
                ProcessData(data);
            }
            catch (TimeoutException) { }
        }
    }

    void ProcessData(string data)
    {
        string[] values = data.Split(',');

        if (values.Length == 7)
        {
            int sensorID = int.Parse(values[0]);
            float accelX = float.Parse(values[1]);
            float accelY = float.Parse(values[2]);
            float accelZ = float.Parse(values[3]);
            float gyroX = float.Parse(values[4]);
            float gyroY = float.Parse(values[5]);
            float gyroZ = float.Parse(values[6]);

            Debug.Log($"Sensor {sensorID} - Accel: ({accelX}, {accelY}, {accelZ}) Gyro: ({gyroX}, {gyroY}, {gyroZ})");

            // Use data to control a GameObject in Unity
            if (sensorID == 0) transform.position = new Vector3(accelX, accelY, accelZ);
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
