using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEmitter : MonoBehaviour
{

    TextMeshProUGUI thisText;
    public ParticleSystem ps;
    public LineRenderer lr;

    public Vector3[] v;

    // Start is called before the first frame update
    void Start()
    {
        thisText = GetComponent<TextMeshProUGUI>();

        StartCoroutine(Drool(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Drool(int amount)
    {
        thisText.ForceMeshUpdate();

        v = ShuffleVerticexArray(thisText.textInfo.meshInfo[0].mesh.vertices);

        Debug.Log(transform.TransformPoint(thisText.textInfo.characterInfo[0].vertex_TL.position));

        int i = 0;
        lr.positionCount = thisText.textInfo.meshInfo[0].mesh.vertices.Length;
        foreach (Vector3 pos in thisText.textInfo.meshInfo[0].mesh.vertices)
        {

            lr.SetPosition(i, transform.TransformPoint(pos));
            i++;

        }



        foreach (Vector3 pos in v)
        {
            if(Random.Range(0f,1f) > 0.9f)
            {
                ps.transform.position = transform.TransformPoint(pos);
                ps.Emit(1);
                yield return new WaitForSeconds(Random.Range(1f,2.5f));
            }
        }
    }

    public Vector3[] ShuffleVerticexArray(Vector3[] array)
    {
        Vector3 tempGO;

        for (int i = 0; i < array.Length; i++)
        {
            int rnd = Random.Range(0, array.Length);
            tempGO = array[rnd];
            array[rnd] = array[i];
            array[i] = tempGO;
        }

        return array;
    }
}
