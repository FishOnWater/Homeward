using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    Vector3 nextPos;
    Vector2 currentPosition;
    int screen;
    public float camSpeed;

    public Camera cam;
    public Transform screen1;
    public Transform screen2;
    public Transform screen3;

    void Start(){
        screen = 1;
        cam.transform.position = screen1.position;
    }

    public int CamTransition(int transition){
        switch (transition)
        {
            case 1:
            if(screen == 1){
                nextPos = screen2.position;
                cam.transform.position = transform.position + nextPos;
                return screen = 2;
            }
            else if(screen == 2)
            {
                nextPos = screen1.position;
                cam.transform.position = transform.position + nextPos;
                return screen = 1;
            }
            else
            {
                return screen = 1;
            }

            case 2:
            if(screen == 2){
                nextPos = screen3.position;
                cam.transform.position = transform.position + nextPos;
                return screen = 3;
            }
            else if(screen == 3)
            {
                nextPos = screen2.position;
                cam.transform.position = transform.position + nextPos;
                return screen = 2;
            }
            else
            {
                return screen = 1;
            }

            default:
            return screen = 1;
        }
    }
}
