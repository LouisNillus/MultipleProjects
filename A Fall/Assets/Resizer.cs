using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Resizer : MonoBehaviour
{

    Vector3 initialScale;


    // Start is called before the first frame update
    void Start()
    {
        initialScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) ScaleUpMultiply(2f, 1f);
        if (Input.GetKeyDown(KeyCode.M)) ScaleUpTo(0.5f, 1f);
    }

    public void ScaleUpMultiply(float multiplier, float duration)
    {
        this.transform.DOScale(this.transform.localScale.x * multiplier, duration);
    }

    public void ScaleUpTo(float value, float duration)
    {
        this.transform.DOScale(value, duration);
    }

    public void ResetScale()
    {

    }
}
