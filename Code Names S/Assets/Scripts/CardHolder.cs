using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardHolder : MonoBehaviour
{

    public Card card;
    public TextMeshProUGUI wordText;
    
    public void PointCard()
    {
        if (GameMaster.instance.gridValidated)
        {
            if (card.color == CardColor.Black) GameMaster.instance.BlackCard();
            if (card.color == CardColor.Yellow) GameMaster.instance.YellowCard();
            if (card.color == CardColor.Blue) GameMaster.instance.BlueCard();
            if (card.color == CardColor.Red) GameMaster.instance.RedCard();
        }
    }

    public void RevealColor()
    {
        if (GameMaster.instance.gridValidated)
            this.GetComponent<Image>().color = CardColorToColor();
    }

    public Color CardColorToColor()
    {
        switch(card.color)
        {
            case CardColor.Blue: return ColorsManager.instance.blue;
            case CardColor.Red: return ColorsManager.instance.red;
            case CardColor.Black:
                wordText.color = Color.white;
                return ColorsManager.instance.black;
            case CardColor.Yellow: return ColorsManager.instance.yellow;
        }

        return Color.white;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) RevealColor();
    }

    public void SetText()
    {
        wordText.text = card.wordName;
    }

    public void Unclickable()
    {
        if(GameMaster.instance.gridValidated)
            this.GetComponent<Button>().interactable = false;
    }

    public void Reroll()
    {
        if (!GameMaster.instance.gridValidated)
        {
            Generator.instance.RerollWord(card);
            SetText();
        }
    }
}
