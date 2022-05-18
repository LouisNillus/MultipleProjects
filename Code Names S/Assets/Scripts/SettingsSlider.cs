using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueText;


    // Start is called before the first frame update
    void Start()
    {
        UpdateSliderText();
    }

    public void UpdateSliderText()
    {
        valueText.text = slider.value.ToString();
    }

    public void Disable()
    {
        slider.interactable = false;
        ColorBlock col = slider.colors;
        col.normalColor = Color.black;
        slider.colors = col;
    }

    public void Enable()
    {
        slider.interactable = true;
        ColorBlock col = slider.colors;
        col.normalColor = Color.white;
        slider.colors = col;
    }

    public void SwitchState()
    {
        if (slider.interactable) Disable();
        else Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
