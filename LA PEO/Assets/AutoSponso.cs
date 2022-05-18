using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSponso : MonoBehaviour
{
    public List<Sprite> sponsors = new List<Sprite>();
    
    // Start
    void Start()
    {
        Roll();
    }

    public void Roll()
    {
        int r = Random.Range(0, sponsors.Count);

        this.GetComponent<Image>().sprite = sponsors[r];        
    }
}
