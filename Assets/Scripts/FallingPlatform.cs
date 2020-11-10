using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    public float dropTimer;
    //public float dropSpeed;
    
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Invoke("DropPlatform", dropTimer);
            Destroy(gameObject, dropTimer + 1f);           
        }
    }

    void DropPlatform()
    {
        //dropping = Vector2.Down + speed * time.deltaTime;

        rb.gravityScale = 5f;
    }
}
