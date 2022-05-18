using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForceText : MonoBehaviour
{
    TextMeshProUGUI title;

    // Start is called before the first frame update
    void Start()
    {
        updateText = true;
        title = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(updateText) title.text = "UNE PROMO EN OR";
    }


    bool updateText = true;
    private void OnDisable()
    {
        updateText = false;
    }
}
