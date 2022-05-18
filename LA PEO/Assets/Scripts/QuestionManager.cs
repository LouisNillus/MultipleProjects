using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{

    public List<Question> questions = new List<Question>();
    public List<AnswerBox> currentAnswers = new List<AnswerBox>();
    public List<TextMeshProUGUI> buttonsText = new List<TextMeshProUGUI>();

    public static QuestionManager instance;

    public Transform answersParent;
    List<GameObject> answersShown = new List<GameObject>();

    public TextMeshProUGUI questionText;

    public GameObject answerTemplate;

    public Question currentQuestion;

    private void Awake()
    {
        instance = this;
    }

    
    public bool winnerRequired = true;

    public void LoadQuestion(Question q)
    {

        currentAnswers.Clear();

        questionText.text = q.question;

        for (int i = 0; i < q.AnswersCount(); i++)
        {
            GameObject go = Instantiate(answerTemplate, answersParent);
            answersShown.Add(go);
            AnswerBox ab = go.GetComponent<AnswerBox>();
            Answer a = ab.Load(q.answers[i], q.GetAnswerPercentage(q.answers[i]));
            buttonsText[i].text = a.answer;
            buttonsText[i].GetComponentInParent<Image>().color = Color.white;
            currentAnswers.Add(ab);
        }

        if(q.AnswersCount() < buttonsText.Count)
        {
            for (int j = q.AnswersCount(); j < buttonsText.Count; j++)
            {
                buttonsText[j].text = "XXXXXXXXX";
                buttonsText[j].GetComponentInParent<Image>().color = Color.gray;
            }
        }

        currentQuestion = q;

        questions.Remove(q);
    }

    public void NextQuestion()
    {
        if(questions.Count > 0)
        {

            if (winnerRequired) return;
            else winnerRequired = true;

            ClearShownAnswers();

            LoadQuestion(questions[Random.Range(0, questions.Count)]);
        }
    }

    public void ClearShownAnswers()
    {
        for (int i = 0; i < answersShown.Count; i++)
        {
            Destroy(answersShown[i]);
        }

        answersShown.Clear();
    }


    public void Reveal(int index)
    {
        AnswerBox ab = currentAnswers[index];

        if(!ab.revealed)
        {
            //ControlPanel.instance.Score(currentQuestion.GetAnswerPercentage(currentQuestion.answers[index]));
            ab.Reveal();
        }

    }

    public int GetAllReveleadAnswerScore()
    {
        int result = 0;
        foreach (AnswerBox ab in currentAnswers)
        {
            if(ab.revealed)
            {
                result += currentQuestion.GetAnswerPercentage(ab.answerData);
            }
        }

        return result;
    }

    // Start
    void Start()
    {
        LoadQuestion(questions[Random.Range(0, questions.Count)]);
    }

    // Update
    void Update()
    {
        
    }
}
