﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Scr_mov : MonoBehaviour
{
    public float movespeed = 15;
    public float JumpForce = 2f;
    public float maxSpeed = 60;
    public float JumpDuration;
    public bool ShootH = false;

    public int action;
    public Camera cam;
    public GameObject crosshairs;
    public Rigidbody2D rb;
    Vector3 movement;
    Vector2 mousePos;
    public Vector2 lookDir;


    // Start is called before the first frame update
    void Start()
    {
        action = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
        
    // Update is called once per frame
    void Update()
    {

        movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //é preciso isto para fazer o cálculo da posição real do rato no mundo
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //cálculo da direção do rato (sim, era mesmo só isto)
        lookDir = mousePos - rb.position;
        crosshairs.transform.position = new Vector2(mousePos.x, mousePos.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {           
            if (isGrounded())
            {
               action = 1;
            }
            else if(IsOnWallLeft()){
                action = 2;
            }else if (IsOnWallRight())
            {
                action = 3;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootH = true;
            ShootHook(lookDir);
        }
    }


    private void FixedUpdate()
    {

        if (IsOnWallLeft()) Debug.Log("Hitting Left Wall");
        if (IsOnWallRight()) Debug.Log("Hitting Right Wall");
        Debug.Log("current speed: " + rb.velocity.magnitude);


        transform.Translate(movement * (Time.deltaTime * movespeed), Space.World);

        switch (action)
        {
            case 1:
                Jump();
            break;

            case 2:
                WallJumpRight();
            break;

            case 3:
                WallJumpLeft();
            break;

            default:
            break;
        }
        if (ShootH)
        {
            ShootHook(lookDir);
            ShootH = false;
        }
        Vector3 vel = rb.velocity;
        if(vel.magnitude > maxSpeed)
        {
            rb.velocity = vel.normalized * maxSpeed;
        }
        
        action = 0;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
    }


    private void WallJumpRight()
    {
        rb.velocity = new Vector2(rb.velocity.x, this.JumpForce);
    }

    private void WallJumpLeft()
    {
        rb.velocity = new Vector2(-rb.velocity.x, this.JumpForce);
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

            gameObject.GetComponent<Rigidbody2D>().AddForce((direction * 3), ForceMode2D.Impulse);
        }
    }


    private bool isGrounded()
    {
        float lenghtToSearch = 0.1f;
        float colliderThreshold = 0.001f;

        Vector2 linestart = new Vector2(this.transform.position.x, this.transform.position.y - GetComponent<Renderer>().bounds.extents.y - colliderThreshold);
        Vector2 vecToSearch = new Vector2(this.transform.position.x, linestart.y - lenghtToSearch);

        RaycastHit2D hitdown = Physics2D.Linecast(linestart, vecToSearch);
        return hitdown;
    }



    private bool IsOnWallLeft()
    {
        bool retval = false;
        float SideLenght = 0.1f;
        float ColliderThreshold = 0.01f;

        Vector2 linestart = new Vector2(this.transform.position.x - GetComponent<Renderer>().bounds.extents.x - ColliderThreshold,0f);
        Vector2 SearchVector = new Vector2(linestart.x-SideLenght,this.transform.position.y);

        RaycastHit2D hitleft = Physics2D.Linecast(linestart, SearchVector);
        retval = hitleft;

        if (retval)
        {
            if (hitleft.collider.GetComponent<NoWallJump>())
            {
                retval = false;
            }
        }
        return retval;
    }

    private bool IsOnWallRight()
    {
        bool retval = false;
        float SideLenght = 0.1f;
        float ColliderThreshold = 0.01f;

        Vector2 linestart = new Vector2(this.transform.position.x + GetComponent<Renderer>().bounds.extents.x + ColliderThreshold, 0f);
        Vector2 SearchVector = new Vector2(linestart.x + SideLenght, this.transform.position.y);

        RaycastHit2D hitleft = Physics2D.Linecast(linestart, SearchVector);
        retval = hitleft;

        if (retval)
        {
            if (hitleft.collider.GetComponent<NoWallJump>())
            {
                retval = false;
            }
        }
        return retval;
    }
}
