using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerMovementVerticalFlip : MonoBehaviour
{
    public float crawlerSpeed;
    public float distance;

    private bool movingRight = true;
    public Transform groundDetection;

    void FixedUpdate(){
        transform.Translate(Vector2.right * crawlerSpeed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, distance);
        Debug.DrawRay(groundDetection.position, Vector2.right);

        if(groundInfo.collider == false){
            if(movingRight == true){
                transform.eulerAngles = new Vector3(180, 180, -90);
                movingRight = false;
            }
            else{
                transform.eulerAngles = new Vector3(0, 180, -90);
                movingRight = true;
            }
        }
    }
}
