using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSend : MonoBehaviour
{
    private string IP;
    public int port;

    IPEndPoint remoteEndPoint;
    UdpClient client;

    string strMessage = "";

    public static UDPSend instance;

    /*private static void Main()
    {
        UDPSend sendObj = new UDPSend();
        sendObj.init();

        sendObj.sendEndless(" endless infos \n");

    }*/

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        init();

        StartCoroutine(UpdateSendString(1f));
    }


    /*void OnGUI()
    {
        Rect rectObj = new Rect(40, 380, 200, 400);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        GUI.Box(rectObj, "# UDPSend-Data\n127.0.0.1 " + port + " #\n"
                    + "shell> nc -lu 127.0.0.1  " + port + " \n"
                , style);


        strMessage = GUI.TextField(new Rect(40, 420, 140, 20), strMessage);
        if (GUI.Button(new Rect(190, 420, 40, 20), "send"))
        {
            sendString(strMessage + "\n");
        }
    }*/


    public void init()
    {
        print("UDPSend.init()");

        IP = "127.0.0.1";
        port = 8051;

        remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, port);
        client = new UdpClient();

        print("Sending to " + IP + " : " + port);
        print("Testing: nc -lu " + IP + " : " + port);

    }

    private void inputFromConsole()
    {
        try
        {
            string text;
            do
            {
                text = Console.ReadLine();

                if (text != "")
                {
                    byte[] data = Encoding.UTF8.GetBytes(text);

                    client.Send(data, data.Length, remoteEndPoint);
                }
            } while (text != "");
        }
        catch (Exception err)
        {
            print(err.ToString());
        }

    }

    public void sendString(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);

            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    private void sendEndless(string testStr)
    {
        do
        {
            sendString(testStr);
        }
        while (true);

    }

    public IEnumerator UpdateSendString(float refreshRate) //Broadcast Only
    {
        while(true)
        {
            if (remoteEndPoint.Address == IPAddress.Broadcast)
            {
                sendString(Generator.instance.encodedGrid);
            }
            else Debug.Log("Send to a specific IP !");

            yield return new WaitForSeconds(refreshRate);
        }
    }

    public void ChangeReceiver(ReceiverType typeOfReceiver, string ip = "")
    {
        switch(typeOfReceiver)
        {
            case ReceiverType.IP:
                remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                client = new UdpClient();
                break;
            case ReceiverType.Broadcast:
                remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, port);
                client = new UdpClient();
                break;
        }
    }

    private void OnApplicationQuit()
    {
        sendString("CLOSE");
    }

}

public enum ReceiverType {IP, Broadcast}
