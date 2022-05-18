using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class FinalManager : MonoBehaviour
{

    public List<Question> duels = new List<Question>();
    public TextMeshProUGUI themeText;
    public List<FinalContainer> containers = new List<FinalContainer>();
    public Color yellow;
    public Question currentQuestion;

    public ScoreBox greenBox;
    public ScoreBox orangeBox;

    public static FinalManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayQuestion()
    {

        HideScores();

        leader = null;
        contester = null;

        currentQuestion = GetRandomQuestion();

        if (currentQuestion == null) return;

        themeText.text = currentQuestion.question;

        roundContainers.Clear();
        roundContainers.AddRange(containers);
        

        int i = 0;

        roundContainers.Shuffle();

        foreach (FinalContainer fc in roundContainers)
        {
            fc.Reset();
            fc.answerText.text = currentQuestion.answers[i].answer;
            fc.score = currentQuestion.answers[i].voters;
            i++;
        }
    }

    public Question GetRandomQuestion()
    {
        if (duels.Count > 0)
        {
            Question q = duels[Random.Range(0, duels.Count)];
            duels.Remove(q);
            return q;
        }
        else return null;

    }


    public FinalContainer leader;
    public FinalContainer contester;
    public List<FinalContainer> roundContainers = new List<FinalContainer>();
    public void Duel()
    {
        if(leader == null)
        {
            leader = roundContainers[Random.Range(0, roundContainers.Count)];
            leader.Leader();
            roundContainers.Remove(leader);
            contester = roundContainers[Random.Range(0, roundContainers.Count)];
            contester.Contester();
            roundContainers.Remove(contester);
        }
        else
        {
            if(roundContainers.Count > 0)
            {
                contester = roundContainers[Random.Range(0, roundContainers.Count)];
                contester.Contester();
                roundContainers.Remove(contester);
            }
        }
    }

    public void Resolve()
    {
        if(contester.score > leader.score)
        {
            leader.Disabled();
            leader = contester;
            contester.Leader();
            Duel();
        }
        else
        {
            contester.Disabled();
            Duel();
        }
    }

    public void PickLeader(FinalContainer fc)
    {
        leader = fc;
        leader.Leader();
        roundContainers.Remove(leader);

        Duel();
    }


    public void StartFinal()
    {
        NextQuestion();
    }

    public void NextQuestion()
    {
        DisplayQuestion();
        //Duel();
    }

    public void Score()
    {
        if(ControlPanel.instance.selectedPlayer == Player.Green) greenBox.Score();
        else orangeBox.Score();
    }

    public void UnScore()
    {
        if (ControlPanel.instance.selectedPlayer == Player.Green) greenBox.UnScore();
        else orangeBox.UnScore();
    }

    public void ShowScores()
    {
        StopAllCoroutines();
        StartCoroutine(ShowScoresCoroutine());
    }

    public IEnumerator ShowScoresCoroutine()
    {
        foreach (FinalContainer fc in containers)
        {
            fc.ShowScore();
            yield return new WaitForSeconds(2);
        }
    }

    public void HideScores()
    {
        foreach (FinalContainer fc in containers)
        {
            fc.HideScore();
        }
    }


}
