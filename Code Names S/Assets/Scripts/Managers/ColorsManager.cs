using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsManager : MonoBehaviour
{

    public Color32 blue;
    public Color32 red;
    public Color32 black;
    public Color32 yellow;
    public Color32 green;


    public static ColorsManager instance;

    void Awake()
    {
        instance = this;
    }
}
