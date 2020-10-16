using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_mov : MonoBehaviour
{
    public float movespeed = 0.025f;
    public float JumpForce = 10f;
    public bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {

    }
        
    // Update is called once per frame
    void Update()
    {      
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.Translate(movement * (Time.deltaTime * movespeed),Space.World);



        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootHook(Input.mousePosition.x, Input.mousePosition.y);
            
        }
    }

    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
    }

    void ShootHook(float xpos, float ypos)
    {
        //this part is fucked
        RaycastHit2D hit = Physics2D.Linecast(transform.position, new Vector2(xpos, ypos),1<<LayerMask.NameToLayer("Action"));
        //this one is working great
        if(hit.collider != null)
        {
            
            Vector2 direction = new Vector2(0, 0);
            direction.x = hit.point.x - transform.position.x  ;
            direction.y = hit.point.y - transform.position.y;

            gameObject.GetComponent<Rigidbody2D>().AddForce((direction * 5), ForceMode2D.Impulse);
        }
    }
}
