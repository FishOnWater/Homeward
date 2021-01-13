using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other){
            if(other.gameObject.CompareTag("Player")){
            rb.AddForce(Vector2.down, (ForceMode2D.Impulse));
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Player hit");
            other.gameObject.GetComponent<PlayerController>().Death();
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("Ground")){
            Debug.Log("Ground hit");
            Destroy(gameObject);
        }
    }
}
