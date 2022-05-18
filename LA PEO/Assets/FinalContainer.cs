using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalContainer : MonoBehaviour
{

    public TextMeshProUGUI answerText;
    public Image outline;

    public Color leaderColor;
    public Color contesterColor;

    public int score;
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Leader()
    {
        outline.gameObject.SetActive(true);
        outline.color = leaderColor;
        answerText.color = leaderColor;
    }

    public void Contester()
    {
        outline.gameObject.SetActive(true);
        outline.color = contesterColor;
        answerText.color = contesterColor;
    }

    public void Disabled()
    {
        outline.gameObject.SetActive(false);
        answerText.color = Color.grey;
    }

    public void Reset()
    {
        outline.gameObject.SetActive(false);
        outline.color = FinalManager.instance.yellow;
        answerText.color = Color.white;
    }

    public void ShowScore()
    {
        scoreText.gameObject.SetActive(true);
        scoreText.text = score.ToString();
    }

    public void HideScore()
    {
        scoreText.gameObject.SetActive(false);
        scoreText.text = "";
    }
}
