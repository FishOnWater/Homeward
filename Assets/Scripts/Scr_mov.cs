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
        transform.position += movement * Time.time * movespeed;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }
    }

    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
    }
}
