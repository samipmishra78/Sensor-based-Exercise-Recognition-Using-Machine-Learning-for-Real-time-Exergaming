import socket
import time

# Set up UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
server_address = ('localhost', 12346)  # Match Unity's listening port

print("UDP Sender started. Enter 0 for Run, 1 for Jump, 2 for Slide. Type 'exit' to quit.")

while True:
    user_input = input("Enter 0 (Run), 1 (Jump), 2 (Slide), or 'exit' to quit: ")

    if user_input == 'exit':
        print("Exiting...")
        break
    elif user_input in ['0', '1', '2']:
        sock.sendto(user_input.encode(), server_address)
        print(f"Sent: {user_input}")
    else:
        print("Invalid input! Please enter 0, 1, or 2.")

    time.sleep(0.1)  # Prevents flooding but ensures regular updates

sock.close()
