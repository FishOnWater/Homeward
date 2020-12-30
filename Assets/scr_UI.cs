using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_UI : MonoBehaviour
{


    public PlayerController player;
    public Text rope,deathtext,timer;
    public int DeathCount;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        DeathCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rope.text = "Ropes : " + player.hooks;
        timer.text = Time.time.ToString("0.00");
        deathtext.text = "DeathCount: " + DeathCount;
        if(Input.GetKeyDown(KeyCode.R))DeathCount++;
    }

    public void WriteRopes()
    {
        
    }
}
