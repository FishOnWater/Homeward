using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawn : MonoBehaviour
{
    public GameObject respawnObj, prefab;
    string tagObj;
    float timer = 0.0f, maxTimer = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Child count: " + respawnObj.transform.childCount);

        if(respawnObj.transform.childCount < 1){
            if(timer <= 0){
            Instantiate(prefab, this.gameObject.transform);
            respawnObj = GameObject.FindGameObjectWithTag("Cycle");
            respawnObj.gameObject.tag = "Untagged";

            timer = maxTimer;
            }
            else
            {
            timer -= Time.deltaTime;   
            }
        }
    }
}
