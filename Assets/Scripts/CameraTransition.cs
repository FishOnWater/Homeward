using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    Vector2 nextPos;
    Vector2 currentPosition;
    int screen;
    float camSpeed;

    public Camera cam;
    public Transform screen1;
    public Transform screen2;
    public Transform screen3;

    void Start(){
        screen = 1;
        cam.position = screen1.position;
    }
    void OnTriggerEnter2D(Collider2D col, int check){
        if(col.gameObject.tag == "Transition1"){
            if(currentPosition == screen1.position){
                nextPos = screen2.position;
                currentPosition = Vector2.MoveTowards(currentPosition, nextPos, camSpeed * time.deltaTime);
                screen = 2;
            }
            
        }
    }
}
