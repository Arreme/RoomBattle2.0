using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem.Controls;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _teamButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _inputMenu;
    [SerializeField] private GameObject _normalMenu;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _modeGameMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private new GameObject camera;
    [SerializeField] private EventSystem _eventSystem;
    //[SerializeField] private GameObject controllerImage;
    //[SerializeField] private GameObject keyboardImage;
    public float transitionSpeed = 5;

    //VECTOR3 DATA
    private Vector3 setOptionsV3 = new Vector3(30.217f, 31.06f, 4.82f);
    private Vector3 mainMenuV3 = new Vector3(19.393f, -15.051f, 2.604f);
    private Vector3 playOptionsV3 = new Vector3(17.765f, -93.826f, -7.904f);
    private Vector3 activateSettingsV3 = new Vector3(17.765f, -93.826f, -7.904f);
    private Vector3 startV3 = new Vector3(17.999f, -46.732f, -7.79f);


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
        camera.transform.eulerAngles = setOptionsV3;
        _normalMenu.SetActive(false);
        _inputMenu.SetActive(true);
        _startMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _backButton.Select();
    }

    public void mainMenuOptions()
    {
        camera.transform.eulerAngles = mainMenuV3;
        _normalMenu.SetActive(true);
        _inputMenu.SetActive(false);
        _startMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _playButton.Select();
    }
    public void backOptions()
    {
        _inputMenu.SetActive(false);
        _normalMenu.SetActive(true);
        _startMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _playButton.Select();
    }

    public void playOptions()
    {
        camera.transform.eulerAngles = playOptionsV3;
        _inputMenu.SetActive(false);
        _normalMenu.SetActive(false);
        _modeGameMenu.SetActive(true);
        _settingsMenu.SetActive(false);
        _teamButton.Select();
    }
    public void activateSettings()
    {
        camera.transform.eulerAngles = activateSettingsV3;
        _inputMenu.SetActive(false);
        _normalMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _settingsMenu.SetActive(true);
        _backMenuButton.Select();
    }
    private void Start()
    {
        StartCoroutine(checkForPressed());
        // NO TOCAR (angulo default de la camara)
        camera.transform.eulerAngles = startV3;
    }

    private IEnumerator checkForPressed()
    {
        bool a = false;
        for (; ; )
        {
            if (Keyboard.current.anyKey.isPressed || (Gamepad.all.Count >= 1 && Gamepad.current.allControls.Any(x => x is ButtonControl button && x.IsPressed() && !x.synthetic)))
            {
                if (!a)
                {
                    _startMenu.SetActive(false);
                    _normalMenu.SetActive(true);
                    _playButton.Select();
                    a = true;
                    camera.transform.eulerAngles = mainMenuV3;
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
