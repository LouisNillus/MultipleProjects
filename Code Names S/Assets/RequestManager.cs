using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    public void AnalyseRequests()
    {
        if(UDPReceive.lastReceivedUDPPacket.Contains("CARDREQUEST"))
        {
            Debug.Log("Received package !");

            string[] result = UDPReceive.lastReceivedUDPPacket.Split('_');

            UDPSend.instance.ChangeReceiver(ReceiverType.IP, result[0]);

            CardInfos ci = new CardInfos(Generator.instance.words[int.Parse(result[2])].wordName, "", "ANSWERCARD");

            UDPSend.instance.sendString(JsonUtility.ToJson(ci));

            UDPSend.instance.ChangeReceiver(ReceiverType.Broadcast);

            UDPReceive.CleanLastPackage();
        }
    }

    private void Update()
    {
        AnalyseRequests();
    }

    //public 
}

[System.Serializable]
public class CardInfos
{
    public string cardName = "";
    public string cardEffect = "";
    public string message = "";

    public CardInfos(string cardName, string cardEffect, string message)
    {
        this.cardName = cardName;
        this.cardEffect = cardEffect;
        this.message = message;
    }
}
