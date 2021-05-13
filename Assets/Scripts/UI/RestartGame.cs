using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    
    public void Exit()
    {
        SceneManager.LoadScene("UI_MENU");
    }

    public void Restart()
    {
        SceneManager.LoadScene("TestScene");
    }
}
