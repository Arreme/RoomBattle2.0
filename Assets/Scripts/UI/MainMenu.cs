using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _inputMenu;
    [SerializeField] private GameObject _normalMenu;
    [SerializeField] private GameObject _startMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene("TeamsSelect");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("UI_MENU");
    }

    public void setOptions()
    {
        _normalMenu.SetActive(false);
        _inputMenu.SetActive(false);
        _startMenu.SetActive(true);
        _startButton.Select();
    }

    public void mainMenuOptions()
    {
        _normalMenu.SetActive(false);
        _inputMenu.SetActive(true);
        _startMenu.SetActive(false);
        _backButton.Select();
    }
    public void backOptions()
    {   
        _inputMenu.SetActive(false);
        _normalMenu.SetActive(true);
        _playButton.Select();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
