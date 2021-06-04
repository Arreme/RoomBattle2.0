using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem.Controls;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _teamButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _inputMenu;
    [SerializeField] private GameObject _normalMenu;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _modeGameMenu;
    public void PlayTeam()
    {
        SceneManager.LoadScene("PickCharacterTeams");
    }

    public void PlayAllvsAll()
    {
        SceneManager.LoadScene("PickCharacterSolo");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("UI_MENU");
    }

    public void setOptions()
    {
        _normalMenu.SetActive(false);
        _inputMenu.SetActive(true);
        _startMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _backButton.Select();
    }

    public void mainMenuOptions()
    {
        _normalMenu.SetActive(true);
        _inputMenu.SetActive(false);
        _startMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _playButton.Select();
    }
    public void backOptions()
    {   
        _inputMenu.SetActive(false);
        _normalMenu.SetActive(true);
        _startMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _playButton.Select();
    }

    public void playOptions()
    {
        _inputMenu.SetActive(false);
        _normalMenu.SetActive(false);
        _modeGameMenu.SetActive(true);
        _teamButton.Select();
    }
    private void Start()
    {
        StartCoroutine(checkForPressed());
    }

    private IEnumerator checkForPressed()
    {
        bool a =false;
        for(; ; )
        {
            if (Keyboard.current.anyKey.isPressed || (Gamepad.all.Count >= 1 && Gamepad.current.allControls.Any(x => x is ButtonControl button && x.IsPressed() && !x.synthetic)))
            {
                if (!a)
                {
                    _startMenu.SetActive(false);
                    _normalMenu.SetActive(true);
                    a = true;
                }
              
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
