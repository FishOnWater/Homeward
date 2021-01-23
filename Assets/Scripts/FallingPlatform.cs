using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("DropPlatform", 1.0f);
            Destroy(gameObject, 3.0f);
        }
    }
    void DropPlatform()
    {
        rb.mass = 1;
        rb.AddForce(Vector2.down, ForceMode2D.Impulse);
    }
}
