using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rope_script : MonoBehaviour
{
    PlayerController hold;

   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            hold = col.gameObject.GetComponent<PlayerController>();
            hold.hooks = hold.Gmax;
            
            Destroy(gameObject);
            

        }
        Debug.Log("collided");
      
    }
    

    
}
