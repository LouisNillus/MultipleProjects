using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GridDrawer : MonoBehaviour
{

    public GridEncoded ge;
    GridEncoded lastGe;

    public GameObject waitingText;

    public GameObject cardTemplate;
    public GameObject gridHandler;

    public CardInfos selectedCard;

    public List<GameObject> cards = new List<GameObject>();

    bool decoded = false;
    bool sendIP = false;

    public static GridDrawer instance;

    public TextMeshProUGUI selectedCardName;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lastGe = new GridEncoded(0, "", "");
    }

    // Update
    void Update()
    {

        if (UDPReceive.instance.lastReceivedUDPPacket.Contains("gridCount"))
        {
            ge = JsonUtility.FromJson<GridEncoded>(UDPReceive.instance.lastReceivedUDPPacket);
        }
        else if (UDPReceive.instance.lastReceivedUDPPacket == "CLOSE")
        {
            Debug.Log("Clean Up !");
            CleanUp();
            this.GetComponent<CanvasScaler>().scaleFactor = 1f;
            waitingText.SetActive(true);
        }
        
        if (UDPReceive.instance.lastReceivedUDPPacket.Contains("ANSWERCARD"))
        {
            Debug.Log("CardInfos loaded !");
            selectedCard = JsonUtility.FromJson<CardInfos>(UDPReceive.instance.lastReceivedUDPPacket);
            UpdateSelectedCardInfos();
        }

        Decode();
        

        if(sendIP == false && ge.ip != "")
        {
            UDPSend.instance.masterIP = ge.ip;
            sendIP = true;
        }
    }

    public void UpdateSelectedCardInfos()
    {
        selectedCardName.text = selectedCard.cardName;
    }

    //TO DO cliquer sur les mots (dans un autre script), pour avoir des options de chaque côté pour les joueurs qui font deviner : "Marquer la carte", "Montrer le mot associé"...

    public void Decode()
    {
        //Debug.Log(lastGe.timeCode + "==>last   " + ge.timeCode + "==>ge");
        if(lastGe.timeCode != ge.timeCode && ge.gridCount != 0)
        {
            waitingText.SetActive(false);

            foreach (GameObject card in cards)
            {
                Destroy(card);
            }

            cards.Clear();

            foreach(int i in ge.colors)
            {
                GameObject go = Instantiate(cardTemplate, gridHandler.transform);
                go.GetComponent<Image>().color = ColorsManager.instance.colors[i];
                cards.Add(go);
            }

            this.GetComponent<CanvasScaler>().scaleFactor = 3 - (0.2f * Mathf.Sqrt(ge.gridCount));

            lastGe = ge;
        }
    }
    
    public void CleanUp()
    {
        foreach (GameObject card in cards)
        {
            Destroy(card);
        }
    }

    public static int GetCardIndex(GameObject go)
    {
        return instance.cards.IndexOf(go);
    }
}
