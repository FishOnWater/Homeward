using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public GameObject virtualCam;

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            virtualCam.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            virtualCam.SetActive(false);
        }
    }
}
