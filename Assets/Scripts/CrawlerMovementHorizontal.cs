using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerMovementHorizontal : MonoBehaviour
{
    public float crawlerSpeed;
    public float distance;

    private bool movingRight = true;
    public Transform groundDetection;

    void FixedUpdate(){
        transform.Translate(Vector2.right * crawlerSpeed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        Debug.DrawRay(groundDetection.position, Vector2.down);

        if(groundInfo.collider == false){
            if(movingRight == true){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else{
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManagerScript.PlaySound("death");
            other.gameObject.GetComponent<PlayerController>().Death();
        }
    }
}
