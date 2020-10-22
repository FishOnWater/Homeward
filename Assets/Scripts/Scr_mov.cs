using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_mov : MonoBehaviour
{
    public float movespeed = 0.025f;
    public float JumpForce = 10f;
    public bool isGrounded = false;

    public Camera cam;
    public GameObject crosshairs;

    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {

    }
        
    // Update is called once per frame
    void Update()
    {      
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.Translate(movement * (Time.deltaTime * movespeed),Space.World);

        //é preciso isto para fazer o cálculo da posição real do rato no mundo
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //cálculo da direção do rato (sim, era mesmo só isto)
        Vector2 lookDir = mousePos - gameObject.GetComponent<Rigidbody2D>().position;
        crosshairs.transform.position = new Vector2(mousePos.x, mousePos.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //ShootHook(Input.mousePosition.x, Input.mousePosition.y);
            ShootHook(lookDir);
        }
    }

    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
    }

    //void ShootHook(float xpos, float ypos)
    void ShootHook(Vector2 mDir)
    {
        //Vector3 diff = mPos - gameObject.transform.position;
        //float distance = diff.magnitude;
        //Vector2 mDir = diff / distance;
        //mDir.Normalize();  

        //this part is fucked
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, mDir, 5000f, 1<<LayerMask.NameToLayer("Action"));
        //this one is working great
        //if(hit.collider != null)
        if(hit)
        {
            Debug.Log("Hit somthing: " + hit.collider.name);
            hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
            
            Vector2 direction = new Vector2(0, 0);
            direction.x = hit.point.x - gameObject.transform.position.x;
            direction.y = hit.point.y - gameObject.transform.position.y;

            gameObject.GetComponent<Rigidbody2D>().AddForce((direction * 5), ForceMode2D.Impulse);
        }
    }
}
