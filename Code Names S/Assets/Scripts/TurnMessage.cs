using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnMessage : MonoBehaviour
{
    public RectTransform endPos;
    public AnimationCurve curve;
    public TextMeshProUGUI message;

    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = this.GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O)) DisplayMessage();
    }

    public void DisplayMessage()
    {
        StopAllCoroutines();
        this.GetComponent<RectTransform>().position = initPos;
        StartCoroutine(TurnSwipe(3f));
    }

    public IEnumerator TurnSwipe(float duration)
    {
        string textToDisplay = GameMaster.instance.currentPlayer.playerColor == CardColor.Blue ? "Blue Team !" : "Red Team !";
        message.color = GameMaster.instance.currentPlayer.playerColor == CardColor.Blue ? ColorsManager.instance.blue : ColorsManager.instance.red;
        message.text = textToDisplay;
        float t = 0f;

        while(t < duration)
        {
            this.GetComponent<RectTransform>().position = Vector3.Lerp(initPos, endPos.position, curve.Evaluate(t/duration));
            yield return null;
            t += Time.deltaTime;
        }
    }
}
