using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Checkpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("checkpoint updated");
            other.gameObject.GetComponent<PlayerController>().checkpoint = this.gameObject;            
        }
    }
 }
