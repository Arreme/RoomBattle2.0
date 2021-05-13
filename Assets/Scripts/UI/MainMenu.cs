using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _inputMenu;
    [SerializeField] private GameObject _normalMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene("PickCharacter");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("UI_MENU");
    }

    public void setOptions()
    {
        _normalMenu.SetActive(false);
        _inputMenu.SetActive(true);
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
