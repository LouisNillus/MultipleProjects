using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridCounter : MonoBehaviour
{

    public Slider slider;
    public Image restBackground;

    public List<Slider> allCards = new List<Slider>();

    public TextMeshProUGUI restText;

    public static GridCounter instance;


    public int value;
    public int restValue;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Generator.instance.SetGridSize(slider);
        UpdateCounterFromSlider();
        UpdateRestFromSlider();
    }

    public void UpdateCounterFromSlider()
    {
        value = (int)Mathf.Pow((int)slider.value, 2);
        this.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
    }

    public void UpdateRestFromSlider()
    {
        int result = 0;
        foreach(Slider s in allCards)
        {
            result += (int)s.value;
        }

        restValue = (int)Mathf.Pow((int)slider.value, 2) - result;

        restBackground.color = restValue > 0 ? ColorsManager.instance.yellow : restValue == 0 ?ColorsManager.instance.green : ColorsManager.instance.red;
        restText.text = (restValue > 0 ? "+" : "") + Mathf.Abs(restValue).ToString();
    }
}
