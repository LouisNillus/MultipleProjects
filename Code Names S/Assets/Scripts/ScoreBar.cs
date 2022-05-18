using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBar : MonoBehaviour
{

    public int blueScore;
    public int redScore;

    public Slider blueCount;
    public Slider redCount;

    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI redScoreText;

    public TextMeshProUGUI winnerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementBlue()
    {
        blueScore++;
        blueScoreText.text = blueScore.ToString();
        VictoryCheck();
    }
    public void IncrementRed()
    {
        redScore++;
        redScoreText.text = redScore.ToString();
        VictoryCheck();
    }

    public void ResetScores()
    {
        redScore = 0;
        redScoreText.text = redScore.ToString();
        blueScore = 0;
        blueScoreText.text = blueScore.ToString();
    }

    public void VictoryCheck()
    {
        if (blueScore >= blueCount.value)
        {
            winnerText.color = ColorsManager.instance.blue;
            winnerText.text = "Blue wins !";
        }
        if (redScore >= redCount.value)
        {
            winnerText.color = ColorsManager.instance.red;
            winnerText.text = "Red wins !";
        }
    }
}
