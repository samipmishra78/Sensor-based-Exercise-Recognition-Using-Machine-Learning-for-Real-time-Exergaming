using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class listener : MonoBehaviour
{
    private UdpClient udpClient;
    private Thread udpThread;
    private int port = 5052; // Match this with your Python script

    public static string receivedMessage = ""; // Stores received data

    private void Start()
    {
        udpClient = new UdpClient(port);
        udpThread = new Thread(new ThreadStart(ListenForData));
        udpThread.IsBackground = true;
        udpThread.Start();
        Application.runInBackground = true;
        
    }

    private void ListenForData()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        while (true)
        {
            try
            {
                byte[] receivedData = udpClient.Receive(ref endPoint);
                receivedMessage = Encoding.UTF8.GetString(receivedData);
                Debug.Log("Received: " + receivedMessage);
            }
            catch (Exception e)
            {
                Debug.LogError("UDP Listener Error: " + e.Message);
            }
        }
    }

    private void OnApplicationQuit()
    {
        udpThread.Abort();
        udpClient.Close();
    }
}
