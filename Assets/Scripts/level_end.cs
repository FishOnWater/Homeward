using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_end : MonoBehaviour
{
    public string SceneName;

    void OnTriggerEnter2D(Collider2D other){
            if(other.gameObject.CompareTag("Player")){
            SceneManager.LoadScene(SceneName);
        }
    }
}
