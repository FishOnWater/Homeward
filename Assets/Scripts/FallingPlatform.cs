﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("DropPlatform", 1f);
            Destroy(gameObject, 2f);
        }
    }
    void DropPlatform()
    {
        rb.AddForce(Vector2.down, (ForceMode2D.Impulse));
    }
}
