using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSend : MonoBehaviour
{

    public string masterIP;
    private string IP;
    public int port;

    IPEndPoint remoteEndPoint;
    UdpClient client;

    string strMessage = "";

    public static UDPSend instance;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        StartCoroutine(WaitForMasterIP());
    }


    public void init()
    {
        print("UDPSend.init()");

        port = 8052;

        remoteEndPoint = new IPEndPoint(IPAddress.Parse(masterIP), port);
        client = new UdpClient();

        print("Master IP =  " + masterIP + " : " + port);
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

    IEnumerator WaitForMasterIP()
    {
        while(masterIP == "")
        {
            Debug.Log("Waiting for master ip...");
            yield return null;
        }

        init();
    }
}
