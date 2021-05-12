using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
