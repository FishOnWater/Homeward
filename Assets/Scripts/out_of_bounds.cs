using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class out_of_bounds : MonoBehaviour
{
        void OnTriggerEnter2D(Collider2D other){
            if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().Death();
        }
    }
}
