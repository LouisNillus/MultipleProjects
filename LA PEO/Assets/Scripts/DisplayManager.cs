using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayManager : MonoBehaviour
{
    public Camera managerCamera;
    public Camera gameCamera;

    public TextMeshProUGUI text;

    void Start()
    {
        if(Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
            text.text = Display.displays.Length.ToString();
        }
    }

    public void SwapDisplays()
    {
        int swap = gameCamera.targetDisplay;
        gameCamera.targetDisplay = managerCamera.targetDisplay;
        managerCamera.targetDisplay = swap;
    }

    private void Update()
    {

    }
}
