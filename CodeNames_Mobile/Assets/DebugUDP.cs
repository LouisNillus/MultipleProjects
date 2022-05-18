using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugUDP : MonoBehaviour
{

    public TextMeshProUGUI waitingText;
    public Gradient grad;
    float t = 0f;
    Material m;

    // Start is called before the first frame update
    void Start()
    {
        waitingText.text = "Waiting for a game...";
        m = waitingText.fontMaterial;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // Update is called once per frame
    void Update()
    {

        if (t > 3f) t = 0f;

        m.SetColor("_UnderlayColor", grad.Evaluate(t/3f));
        t += Time.deltaTime;
        waitingText.fontMaterial = m;
    }
}
