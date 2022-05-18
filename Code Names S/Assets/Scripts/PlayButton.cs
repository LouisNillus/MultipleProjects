using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{

    public GameObject gameSettings;
    public Image restBackground;


    // Start is called before the first frame update
    void Start()
    {
        //Invoke("LaunchGame", 20f); // == AUTO LAUNCH
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchGame()
    {
        if (GridCounter.instance.restValue < 0)
        {
            return;
        }
        else
        {
            Generator.instance.StartGame();
            gameSettings.SetActive(false);
        }
    }
}
