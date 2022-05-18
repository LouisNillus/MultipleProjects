using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VisualChanger : MonoBehaviour
{

    SpriteRenderer visual;
    Tween currentFade;
    Color initialColor;
    Sprite initialSprite;
    public Sprite sad;

    // Start is called before the first frame update
    void Start()
    {
        visual = GetComponent<SpriteRenderer>();
        initialColor = visual.color;
        initialSprite = visual.sprite;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Fade(TypeOfFade.In, 3f);
        if (Input.GetKeyDown(KeyCode.Z))
            Fade(TypeOfFade.Color, 2f, Color.red);
        if (Input.GetKeyDown(KeyCode.E))
            Fade(TypeOfFade.Out, 3f);

        if (Input.GetKeyDown(KeyCode.R))
            ResetColor(0.5f);
        if (Input.GetKeyDown(KeyCode.Y))
            SpriteSwap(sad);
        if (Input.GetKeyDown(KeyCode.U))
            SpriteSwap(initialSprite);
    }

    public void Fade(TypeOfFade typeOfFade, float duration, Color color = default)
    {
        currentFade.Kill();

        switch (typeOfFade)
        {
            case TypeOfFade.Out:
                currentFade = visual.DOColor(new Color(visual.color.r, visual.color.g, visual.color.b, 0f), duration);
                break;
            case TypeOfFade.Mid:
                currentFade = visual.DOColor(new Color(visual.color.r, visual.color.g, visual.color.b, 0.5f), duration);
                break;
            case TypeOfFade.In:
                currentFade = visual.DOColor(new Color(visual.color.r, visual.color.g, visual.color.b, 1f), duration);
                break;
            case TypeOfFade.Color:
                currentFade = visual.DOColor(color, duration);
                break;
        }
    }

    public void SpriteSwap(Sprite _sprite)
    {
        visual.sprite = _sprite;
    }

    private void ResetColor(float duration)
    {
        Fade(TypeOfFade.Color, duration, initialColor);       
    }

    private void ResetSprite()
    {
        SpriteSwap(initialSprite);
    }
}
public enum TypeOfFade { Out, Mid, In, Color}
