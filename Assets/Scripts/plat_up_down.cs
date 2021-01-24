using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat_up_down : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pos2.position)
            nextPos = pos1.position;

        if(transform.position == pos1.position)
            nextPos = pos2.position;

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);   
    }

    private void OnDrawGizmos(){
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.collider.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        other.collider.transform.SetParent(null);
    }
}
