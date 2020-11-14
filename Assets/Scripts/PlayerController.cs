using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    
    private float moveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;
    
    //Cria um circulo que irá verificar se está a tocar no chão
    private bool isGrounded;
    public Transform groundCheck;
    public float checkReadius;
    public LayerMask whatIsGround;

    //Se quisermos double jumps ou mais
    private int extraJumps;
    public int extraJumpsValue;

    //Acesso ao script da Câmara
    public CameraTransition camScript;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
    }

    void FixedUpdate(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkReadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal"); //GetAxisRaw for more snappy move
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0){
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    //Para já não se consegue ver, mas fica aqui já apontado
    void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded == true){
            extraJumps = extraJumpsValue;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0){
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }  
        else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true){
            rb.velocity = Vector2.up * jumpForce;
        } 
    }

    void OnTriggerEnter2D(Collider2D col){
        int transCheck = 0;

        if(col.CompareTag("Transition1")){
            transCheck = 1;
        }
        if(col.CompareTag("Transition2")){
            transCheck = 2;
        }

        camScript.CamTransition(transCheck);
    }
}
