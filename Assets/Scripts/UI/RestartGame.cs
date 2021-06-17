using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    
    public void Exit()
    {
        SceneManager.LoadScene("UI_MENU");
        Destroy(PlayerConfigManager.Instance.gameObject);
    }

    public void Restart()
    {
        SceneManager.LoadScene("FinalV3");
        PlayerConfigManager.Instance.runAnimation = false;
    }
}
