using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private bool inputDelay = true;

    private void Start()
    {
        StartCoroutine(delay());
    }

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        inputDelay = false;
    }

    public void Exit()
    {
        if (inputDelay) return;
        SceneManager.LoadScene("UI_MENU");
        Destroy(PlayerConfigManager.Instance.gameObject);
    }

    public void Restart()
    {
        if (inputDelay) return;
        PlayerConfigManager.Instance._deadPlayers.Clear();
        PlayerConfigManager.Instance.runAnimation = false;
        SceneManager.LoadScene("FinalV3");
    }
}
