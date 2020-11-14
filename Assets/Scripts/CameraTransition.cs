using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    Vector2 nextPos;
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

    public void CamTransition(int transition){
        switch (transition)
        {
            case 1:
            if(screen == 1){
                nextPos = screen2.position;
                currentPosition = Vector2.MoveTowards(currentPosition, nextPos, camSpeed * Time.deltaTime);
                screen++;
            }
            else if(screen == 2)
            {
                nextPos = screen1.position;
                currentPosition = Vector2.MoveTowards(currentPosition, nextPos, camSpeed * Time.deltaTime);
                screen--;
            }
            else
            {
                break;
            }
            break;

            case 2:
            if(screen == 2){
                nextPos = screen3.position;
                currentPosition = Vector2.MoveTowards(currentPosition, nextPos, camSpeed * Time.deltaTime);
                screen++;
            }
            else if(screen == 3)
            {
                nextPos = screen2.position;
                currentPosition = Vector2.MoveTowards(currentPosition, nextPos, camSpeed * Time.deltaTime);
                screen--;
            }
            else
            {
                break;
            }
            break;

            default:
            break;
        }
    }
}
