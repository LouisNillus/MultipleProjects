using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public string wordName;
    public CardColor color = CardColor.Yellow;
    public Category categories;
    public bool found;
    public CardHolder holder;

    public Card(string _wordName, CardColor _cardColor)
    {
        wordName = _wordName;
        color = _cardColor;
    }
}
