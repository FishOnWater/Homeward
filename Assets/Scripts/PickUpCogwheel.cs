using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCogwheel : MonoBehaviour
{
    PlayerController hold;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            
            hold = col.gameObject.GetComponent<PlayerController>();
            hold.cogwheels++;
            SoundManagerScript.PlaySound("pickUpCogwheel");
            Destroy(this.gameObject);
        }
        Debug.Log("collided");

    }
}
