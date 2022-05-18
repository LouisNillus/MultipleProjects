using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Team : MonoBehaviour
{

    public List<Image> crossList = new List<Image>();
    public int crossIndex = 0;
    public GameObject selectedFrame;
    public TextMeshProUGUI scoreText;

    public int score;
    int tempScore;

    public void Mistake()
    {
        if(crossIndex < crossList.Count) crossList[crossIndex].color = Color.red;
        
        crossIndex++;        
    }

    public void ResetCross()
    {
        foreach (Image cross in crossList)
        {
            cross.color = Color.white;
        }
        crossIndex = 0;
    }

    public void SaveScore()
    {
        score = tempScore;
        scoreText.text = score.ToString();
    }

    public void RevertScore()
    {
        tempScore = score;
        scoreText.text = score.ToString();
    }

    public void UpdateScore(int points)
    {
        tempScore += points;
        scoreText.text = tempScore.ToString();
    }

    public int RoundScore()
    {
        return tempScore - score;
    }

    // Start
    void Start()
    {
        
    }

    // Update
    void Update()
    {
        
    }
}
