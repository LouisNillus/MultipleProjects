using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventZone : MonoBehaviour
{

    public string tag;
    public Color gizmosColor;


    public bool triggerOnce;
    bool canTrigger = true;

    public UnityEvent onEnter;
    public UnityEvent onStay;
    public UnityEvent onExit;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tag) && canTrigger)
        {
            onEnter.Invoke();

            if (triggerOnce) canTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(tag) && canTrigger)
        {
            onStay.Invoke();

            if (triggerOnce) canTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(tag) && canTrigger)
        {
            onExit.Invoke();

            if (triggerOnce) canTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag) && canTrigger)
        {
            onEnter.Invoke();

            if (triggerOnce) canTrigger = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag) && canTrigger)
        {
            onStay.Invoke();

            if (triggerOnce) canTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag) && canTrigger)
        {
            onExit.Invoke();

            if (triggerOnce) canTrigger = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawCube(this.GetComponent<BoxCollider2D>().bounds.center, this.GetComponent<BoxCollider2D>().bounds.size);
    }
}
