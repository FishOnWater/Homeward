using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDmgPlayer : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManagerScript.PlaySound("death");

            other.gameObject.GetComponent<PlayerController>().Death();
        }
    }
}
