using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsManager : MonoBehaviour
{
    public List<Color32> colors = new List<Color32>();

    public static ColorsManager instance;

    void Awake()
    {
        instance = this;
    }
}
