using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    BoxCollider2D col;
    Transform t;
    SpriteRenderer sr;

    public ParticleSystem fallPS;

    public LayerMask ground;
    public float speed;
    float initialSpeed;
    public float airSpeed;
    public float jumpForce;

    public bool jump = false;
    public bool fallen = true;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesHitTriggers = false;
        initialSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        t = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

#if UNITY_EDITOR
        Debug.DrawRay(this.transform.position + (Vector3.right * (col.size.x / 2 - 0.02f)), Vector2.down * (col.size.y / 2 * this.transform.localScale.y + 0.1f), Color.blue);
        Debug.DrawRay(this.transform.position + (Vector3.left * (col.size.x / 2 - 0.02f)), Vector2.down * (col.size.y / 2 * this.transform.localScale.y + 0.1f), Color.blue);
#endif

        if (Input.GetKeyDown(KeyCode.Space)) jump = true;
    }


    int additionalParticles = 0;
    bool isAddingFallParticles = false;
    private void FixedUpdate()
    {
        if(IsGrounded() && fallen == false)
        {
            fallen = true;

            Debug.Log(30 + additionalParticles);
            //fallPS.Emit(30 + additionalParticles);
            fallPS.Play();
            //additionalParticles = 0;
        }

        if (jump && IsGrounded()) Jump();
        else jump = false;

        if (!IsGrounded())
        {
            //if(isAddingFallParticles == false) StartCoroutine(MoreFallParticles(5));
            fallen = false;
            speed = airSpeed;
        } 
        else speed = initialSpeed;

    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.D)) t.position += Vector3.right * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Q)) t.position += Vector3.left * speed * Time.deltaTime;
    }

    public bool IsGrounded()
    {
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(this.transform.position + (Vector3.right * (col.size.x / 2 - 0.02f)), Vector2.down, col.size.y/2 * this.transform.localScale.y + 0.1f, ground);
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(this.transform.position + (Vector3.left * (col.size.x / 2 - 0.02f)), Vector2.down, col.size.y/2 * this.transform.localScale.y + 0.1f, ground);

        if (hitsLeft.Length > 0 || hitsRight.Length > 0) return true;
        else return false;
    }
    
    public void Jump()
    {
        jump = false;
        fallen = false;
        rb.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    IEnumerator MoreFallParticles(int quota100ms)
    {
        isAddingFallParticles = true;
        while (fallen == false)
        {
            additionalParticles += quota100ms;
            yield return new WaitForSeconds(0.1f);
        }
        isAddingFallParticles = false;
    }
}
