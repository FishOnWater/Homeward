using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btn_start : MonoBehaviour
{
   public void Change_Scene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }  
}
