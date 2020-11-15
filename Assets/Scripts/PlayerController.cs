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
    //extended jump variables
    [SerializeField] bool isJumpHeld;    
    [SerializeField] float MaxJumpTime = 1f;
    private float CurrentJumpTime;

    //walljump variables
    [SerializeField] float maxLockoutTime = 0.1f;
    float timer;
    //Acesso ao script da Câmara
    public CameraTransition camScript;

    //variaveis de deteção de parede
    float SideLenght = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        CurrentJumpTime = 0f;
    }

    void FixedUpdate(){
        if(timer <= 0) { 
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkReadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal"); //GetAxisRaw for more snappy move
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if(facingRight == false && moveInput > 0){
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }

        if (timer > 0f) timer -= Time.deltaTime;
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
            isJumpHeld = false;
        }

        if (timer <= 0)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
            {
                rb.velocity = Vector2.up * jumpForce;
                isJumpHeld = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (facingRight)
                    {
                        if (IsOnWallRight())
                        {
                            timer = maxLockoutTime;
                            rb.velocity = new Vector2(-speed, 1f * jumpForce);
                        }
                    }
                    else
                    {
                        if (IsOnWallLeft())
                        {
                            timer = maxLockoutTime;
                            rb.velocity = new Vector2(speed, 1f * jumpForce);
                        }
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumpHeld = false;
            CurrentJumpTime = 0f;
        }


        if(isJumpHeld && CurrentJumpTime < MaxJumpTime)
        {
            Debug.Log("a correr");
            CurrentJumpTime += Time.deltaTime;
            rb.velocity += Vector2.up * (jumpForce/200f);
        }

        if(CurrentJumpTime > MaxJumpTime)
        {
            isJumpHeld = false;
            CurrentJumpTime = 0f;
        }



        if (facingRight)
        {
            if (IsOnWallRight())
            {
               Debug.Log("detetada parede a direita");
            }
        }else{
            if (IsOnWallLeft())
            {
                Debug.Log("detetada parede a esquerda");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        int transCheck = 0;
        int screenCheck = 0;

        if(col.CompareTag("Transition1")){
            transCheck = 1;
        }
        if(col.CompareTag("Transition2")){
            transCheck = 2;
        }

        screenCheck = camScript.CamTransition(transCheck);
    }

    private bool IsOnWallRight()
    {
        bool retval = false;
        
        float ColliderThreshold = 0.01f;

        Vector2 linestart = new Vector2(this.transform.position.x + GetComponent<Renderer>().bounds.extents.x + ColliderThreshold, this.transform.position.y);
        Vector2 SearchVector = new Vector2(linestart.x + SideLenght, this.transform.position.y);
        //Debug.DrawRay(linestart, SearchVector);
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

    private bool IsOnWallLeft()
    {
        bool retval = false;
        
        float ColliderThreshold = 0.01f;

        Vector2 linestart = new Vector2(this.transform.position.x - GetComponent<Renderer>().bounds.extents.x - ColliderThreshold, this.transform.position.y);
        Vector2 SearchVector = new Vector2(linestart.x - SideLenght, this.transform.position.y);
        //Debug.DrawRay(linestart, SearchVector);
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
