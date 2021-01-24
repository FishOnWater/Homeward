using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Checkpoint : MonoBehaviour
{
 
    public int count;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Debug.Log("checkpoint updated");
            
            if (count > other.gameObject.GetComponent<PlayerController>().checkCount)
            {
                SoundManagerScript.PlaySound("checkpoint");
                other.gameObject.GetComponent<PlayerController>().checkpoint = this.gameObject;
                other.gameObject.GetComponent<PlayerController>().checkCount = count;
            }


        }
    }
}
