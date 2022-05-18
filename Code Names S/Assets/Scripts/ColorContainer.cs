using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ColorContainer
{
    [Range(0,50)]
    public int count;
    public CardColor cardColor;

    public Slider slider;

    public void SetValue()
    {
        count = (int)slider.value;
    }
}
