using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPanel : MonoBehaviour
{
        
    public Image background;
    public Color green;
    public Color orange;

    public Team greenTeam;
    public Team orangeTeam;

    public GameObject totalScoreGreen;
    public GameObject totalScoreOrange;

    public GameObject greenTurnFinal;
    public GameObject orangeTurnFinal;

    public Player selectedPlayer = Player.Green;

    public static ControlPanel instance;

    public Toggle addOpponentScore;

    private void Awake()
    {
        instance = this;
    }

    // Start
    void Start()
    {
        background.color = green;
    }

    // Update
    void Update()
    {
        if(selectedPlayer == Player.Green)
        {
            totalScoreGreen.SetActive(true);
            greenTurnFinal.SetActive(true);
            totalScoreGreen.GetComponentInChildren<TextMeshProUGUI>().text = QuestionManager.instance.GetAllReveleadAnswerScore().ToString();
            totalScoreOrange.GetComponentInChildren<TextMeshProUGUI>().text = QuestionManager.instance.GetAllReveleadAnswerScore().ToString();
            totalScoreOrange.SetActive(false);
            orangeTurnFinal.SetActive(false);
        }
        else
        {
            totalScoreOrange.SetActive(true);
            orangeTurnFinal.SetActive(true);
            totalScoreOrange.GetComponentInChildren<TextMeshProUGUI>().text = QuestionManager.instance.GetAllReveleadAnswerScore().ToString();
            totalScoreGreen.GetComponentInChildren<TextMeshProUGUI>().text = QuestionManager.instance.GetAllReveleadAnswerScore().ToString();
            totalScoreGreen.SetActive(false);
            greenTurnFinal.SetActive(false);
        }
    }

    public void AddMistake()
    {
        if(selectedPlayer == Player.Green)
        {
            greenTeam.Mistake();
        }
        else
        {
            orangeTeam.Mistake();
        }
    }

    public void ResetMistakes()
    {
        orangeTeam.ResetCross();
        greenTeam.ResetCross();
    }

    public void ClearCurrentTeamMistakes()
    {
        if(selectedPlayer == Player.Orange) orangeTeam.ResetCross();
        else greenTeam.ResetCross();
    }


    public void SelectPlayer()
    {
        selectedPlayer = selectedPlayer == Player.Green ? Player.Orange : Player.Green;

        greenTeam.selectedFrame.SetActive(selectedPlayer == Player.Green);
        orangeTeam.selectedFrame.SetActive(selectedPlayer == Player.Orange);


        background.color = selectedPlayer == Player.Green ? green : orange;
    }

    public void OpenClosePanel(GameObject go)
    {
        go.SetActive(!go.activeInHierarchy);
    }

    public void AddScore(int points)
    {
        if (selectedPlayer == Player.Green)
        {
            greenTeam.UpdateScore(points);
            greenTeam.SaveScore();
        }
        else
        {
            orangeTeam.UpdateScore(points);
            orangeTeam.SaveScore();
        }
    }

    public void Score(int points)
    {
        if(selectedPlayer == Player.Green)
        {
            greenTeam.UpdateScore(points);
        }
        else
        {
            orangeTeam.UpdateScore(points);
        }
    }


    public void Winner()
    {
        if(QuestionManager.instance.winnerRequired)
        {
            if (selectedPlayer == Player.Green)
            {
                greenTeam.UpdateScore(QuestionManager.instance.GetAllReveleadAnswerScore());
            }
            else
            {
                orangeTeam.UpdateScore(QuestionManager.instance.GetAllReveleadAnswerScore());
            }
            
            QuestionManager.instance.winnerRequired = false;
        }
    }
}

public enum Player {Green, Orange}
