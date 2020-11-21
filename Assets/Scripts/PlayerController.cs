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

    //variaveis de deteção de parede
    float SideLenght = 0.05f;

        //grappling hook

        public Camera cam;
        public GameObject crosshairs;
        public Vector2 lookDir;
        Vector2 mousePos;
        //Vector2 resultingdir;
        Vector2 targetPos;

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
        //colocar aqui
        if (facingRight == false && moveInput > 0){
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
        }

        //é preciso isto para fazer o cálculo da posição real do rato no mundo
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //cálculo da direção do rato (sim, era mesmo só isto)
        lookDir = mousePos - rb.position;
        crosshairs.transform.position = new Vector2(mousePos.x, mousePos.y);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //resultingdir = ShootHook(lookDir);
            targetPos = ShootHook(lookDir);
            Debug.Log("Direção do ganhco: " + targetPos);
            //rb.AddForce(resultingdir, ForceMode2D.Impulse);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5.0f*Time.deltaTime);                 
        }

        /*if(resultingdir != null)
        {
            Debug.DrawRay(rb.position, resultingdir);
        }*/

        if(targetPos != null)
        {
            Debug.DrawRay(rb.position, targetPos);
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


        /*debug code
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
        */
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

    public Vector2 ShootHook(Vector2 mDir)
    {
        //Vector3 diff = mPos - gameObject.transform.position;
        //float distance = diff.magnitude;
        //Vector2 mDir = diff / distance;
        //mDir.Normalize();  

        //this part is fucked
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, mDir, 5000f, 1 << LayerMask.NameToLayer("Action"));
        //this one is working great
        //if(hit.collider != null)
        if (hit)
        {
            Debug.Log("Hit somthing: " + hit.collider.name);
            hit.transform.GetComponent<SpriteRenderer>().color = Color.red;

            //Vector2 direction = new Vector2(0, 0);
            //direction.x = hit.point.x - gameObject.transform.position.x;
            //direction.y = hit.point.y - gameObject.transform.position.y;
           
           Vector2 hitPos = new Vector2(hit.point.x, hit.point.y);

            return hitPos;
        }

        return Vector2.zero;
    }
}