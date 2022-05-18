using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public List<Player> players = new List<Player>();

    [ReadOnly]
    public bool gridValidated = false;

    public UnityEvent blackCard;
    public UnityEvent yellowCard;
    public UnityEvent redCard;
    public UnityEvent blueCard;
    public UnityEvent onGridValidation;

    public Player currentPlayer;
    public int currentPlayerIndex = 0;

    public TurnMessage message;

    public static GameMaster instance;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeTurn()
    {
        currentPlayer = players[currentPlayerIndex + 1 < players.Count ? currentPlayerIndex + 1 : 0];
        currentPlayerIndex = players.IndexOf(currentPlayer);
        message.DisplayMessage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) ChangeTurn();   
    }

    public void BlackCard()
    {
        blackCard.Invoke();
    }
    
    public void YellowCard()
    {
        yellowCard.Invoke();
    }

    public void RedCard()
    {
        redCard.Invoke();
    }

    public void BlueCard()
    {
        blueCard.Invoke();
    }

    public void ValidateGrid()
    {
        gridValidated = true;
        onGridValidation.Invoke();
    }

    public void RevealAllCards()
    {
        foreach(Card c in Generator.instance.words)
        {
            c.holder.RevealColor();
            c.holder.Unclickable();
        }
    }
}
