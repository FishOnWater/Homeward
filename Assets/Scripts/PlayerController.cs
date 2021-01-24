using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Animator animator;

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

    //walljump variables
    [SerializeField] float maxLockoutTime = 0.1f;
    float timer;

    public GameObject checkpoint;
    int deaths;

    //variaveis de deteção de parede
    float SideLenght = 0.05f;

    //grappling hook
    public Camera cam;
    public GameObject crosshairs;
    public Vector2 lookDir;
    Vector2 mousePos;
    Vector2 resultingdir;
    [SerializeField] float GTimeMax;
    [SerializeField] float GTime;
    bool grappling;
    public int Gmax = 3;
    public int hooks;
    public int boostdivider;
    public int GPMultiplier;
    // cogwheel

    public int cogwheels;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        //CurrentJumpTime = 0f;
        grappling = false;
        hooks = Gmax;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (timer <= 0)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkReadius, whatIsGround);
            if (!grappling)
            {
                if (isGrounded)
                {

                    moveInput = Input.GetAxis("Horizontal"); //GetAxisRaw for more snappy move
                    animator.SetFloat("horizontal", Mathf.Abs(moveInput));
                    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
                }
                else
                {
                    moveInput = Input.GetAxis("Horizontal");
                    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
                }
            }
            else
            {
                GTime -= Time.deltaTime;
                if (GTime <= 0) grappling = false;
            }
        }
        //colocar aqui
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        if (timer > 0f) timer -= Time.deltaTime;
        animator.SetFloat("special time", timer);
        animator.SetBool("isGrap", grappling);
    }

    //Para já não se consegue ver, mas fica aqui já apontado
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("isjumping", isJumpHeld);
        animator.SetBool("Grounded", isGrounded);
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;

        }

        //é preciso isto para fazer o cálculo da posição real do rato no mundo
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //cálculo da direção do rato (sim, era mesmo só isto)
        lookDir = mousePos - rb.position;
        crosshairs.transform.position = new Vector2(mousePos.x, mousePos.y);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hooks > 0)
            {
                hooks--;
                resultingdir = ShootHook(lookDir);
                resultingdir.Normalize();
                Debug.Log("Direção do gancho: " + resultingdir);
                rb.velocity = Vector2.zero;
                rb.AddForce(resultingdir * speed * GPMultiplier, ForceMode2D.Impulse);
                grappling = true;
                GTime = GTimeMax;
            }
        }

        if (timer <= 0)
        {

            if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
            {
                rb.velocity = Vector2.up * jumpForce;
                SoundManagerScript.PlaySound("jump");
                //isJumpHeld = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (facingRight)
                    {
                        if (IsOnWallRight())
                        {
                            timer = maxLockoutTime;
                            rb.velocity = new Vector2(-speed, 1.5f * jumpForce);
                            SoundManagerScript.PlaySound("walljump");
                        }
                    }
                    else
                    {
                        if (IsOnWallLeft())
                        {
                            timer = maxLockoutTime;
                            rb.velocity = new Vector2(speed, 1.5f * jumpForce);
                            SoundManagerScript.PlaySound("walljump");

                        }
                    }
                }
            }
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

            Vector2 direction = new Vector2(0, 0);
            direction.x = hit.point.x - gameObject.transform.position.x;
            direction.y = hit.point.y - gameObject.transform.position.y;


            return direction;
        }

        return Vector2.zero;
    }

    public void Death()
    {
        transform.position = checkpoint.transform.position;
        GameObject.FindGameObjectWithTag("HUD").GetComponent<scr_UI>().DeathCount++;
        cogwheels = 0;
    }
}