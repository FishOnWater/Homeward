using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private enum State
    {
        Roaming,
        ChaseTarget,
        Returning,
    }

    private Vector3 startPos, savedStartPos;
    private Vector3 roamPos;
    private Vector3 NextDestination;
    private State state;
    [SerializeField]
    public float speed;
    [SerializeField]
    public float targetRange;


    public GameObject player;
    private Vector3 playerPos;

    public float startX, startY;

    private void Awake()
    {
        state = State.Roaming;
    }
    void Start()
    {
        NextDestination = Vector3.zero;
        savedStartPos = startPos;
        startPos = transform.position;
        roamPos = GetRoamingPos();
    }


    void Update()
    {
        playerPos = player.transform.position;


        switch (state)
        {
            default:
            case State.Roaming:
                
                if (ReachedRandomPos())
                {
                    roamPos = GetRoamingPos();
                }

                FindTarget();
                NpcDirection(roamPos, transform.position);
                transform.position = Vector3.MoveTowards(transform.position, roamPos, Time.deltaTime * speed);
                break;
            case State.ChaseTarget:
                FindTarget();
                NpcDirection(playerPos, transform.position);
                transform.position = Vector3.MoveTowards(transform.position, playerPos, Time.deltaTime * speed);
                break;
            case State.Returning:
                FindTarget();
                NpcDirection(playerPos, transform.position);
                GetStartPos();
                transform.position = Vector3.MoveTowards(transform.position, savedStartPos, Time.deltaTime * speed);
                break;
        }

    }
    private bool ReachedRandomPos()
    {
        if (transform.position == roamPos)
            return true;
        else
            return false;
    }
    private Vector3 GetRoamingPos()
    {
        return startPos + GetRandomDir() * Random.Range(10f, 10f);

    }
    private void GetStartPos()
    {
        if (Vector3.Distance(transform.position, savedStartPos) > 0)
        {
            state = State.Returning;
        }
        else
            state = State.Roaming;
    }


    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
    private void FindTarget()
    {
        
        if (Vector3.Distance(transform.position, playerPos) < targetRange)
        {
            state = State.ChaseTarget;
        }
        else
            state = State.Roaming;
    }



    private void NpcDirection(Vector3 currentPos, Vector3 destinationPos)
    {
        if ((destinationPos.x - currentPos.x) > 0)
            transform.eulerAngles = new Vector3(0, -180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManagerScript.PlaySound("death");
            other.gameObject.GetComponent<PlayerController>().Death();
        }
    }

}
