using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBox : MonoBehaviour
{

    public List<Image> boxes = new List<Image>();
    public int score;
    public Color teamColor;

    public void Score()
    {
        if(score < boxes.Count && score >= 0)
        {
            boxes[score].color = teamColor;
            score++;
        }
    }

    public void UnScore()
    {
        score --;
        score = Mathf.Clamp(score, 0, 10);
        boxes[score].color = Color.white;
    }
}
