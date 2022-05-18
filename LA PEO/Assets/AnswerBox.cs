using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerBox : MonoBehaviour
{

    public Image reveal;
    public TextMeshProUGUI answer;
    public TextMeshProUGUI points;
    public bool revealed;
    public Answer answerData;

    private void Start()
    {

    }

    public void Reveal()
    {
        revealed = true;
        StartCoroutine(RevealAnswer(1f));
    }

    public IEnumerator RevealAnswer(float duration)
    {
        float time = 0f;

        while(time < duration)
        {
            yield return null;
            time += Time.deltaTime;
            reveal.fillAmount = 1f - (time/duration);
        }

    }

    public Answer Load(Answer a, int _points)
    {
        answerData = a;
        answer.text = a.answer;
        points.text = _points.ToString();

        return a;
    }

}
