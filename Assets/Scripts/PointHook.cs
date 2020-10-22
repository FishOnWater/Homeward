using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHook : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject crosshairs;
    private Vector3 mouseTarget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseTarget = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector2(mouseTarget.x, mouseTarget.y);   
    }
}
