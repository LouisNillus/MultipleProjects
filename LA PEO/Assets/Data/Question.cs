using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class Question : ScriptableObject
{
    [TextArea(1,3)]
    public string question;
    public QuestionType type;
    public List<Answer> answers = new List<Answer>();

    public int otherVoters;

    public int GetAnswerPercentage(Answer a)
    {
        return Mathf.RoundToInt((float)(a.voters) / (float)GetVotersCount() * 100f);
    }

    public int GetVotersCount()
    {
        int result = 0;

        foreach(Answer a in answers)
        {
            result += a.voters;
        }

        result += otherVoters;

        return result;
    }

    public int AnswersCount()
    {
        return answers.Count;
    }
}

[System.Serializable]
public class Answer
{
    public string answer;
    public int voters;
}

public enum QuestionType{Classic, Election}