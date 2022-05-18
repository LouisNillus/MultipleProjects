using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSelector : MonoBehaviour
{

    public static CardSelector instance;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestCardInfo(GameObject go)
    {
        int index = GridDrawer.GetCardIndex(go);

        UDPSend.instance.sendString(Network.GetLocalIPAddress() + "_CARDREQUEST_" + index.ToString());
    }
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

