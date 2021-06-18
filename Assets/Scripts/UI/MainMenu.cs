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
    [SerializeField] private Button _settingsButton;
    [SerializeField] private GameObject _inputMenu;
    [SerializeField] private GameObject _normalMenu;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _modeGameMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private new GameObject camera;
    [SerializeField] private EventSystem _eventSystem;
    private Animation anim;
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
        anim.Play("controls");
        _normalMenu.SetActive(false);
        _inputMenu.SetActive(true);
        _startMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _backButton.Select();
    }

    public void mainMenuOptions()
    {
        anim.Play("backFromModes");
        _normalMenu.SetActive(true);
        _inputMenu.SetActive(false);
        _startMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _playButton.Select();
    }
    public void backOptions()
    {
        anim.Play("backToMain");
        _inputMenu.SetActive(false);
        _normalMenu.SetActive(true);
        _startMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _playButton.Select();
    }

    public void playOptions()
    {
        anim.Play("modeGame");
        _inputMenu.SetActive(false);
        _normalMenu.SetActive(false);
        _modeGameMenu.SetActive(true);
        _settingsMenu.SetActive(false);
        _teamButton.Select();
    }

    public void activateSettings()
    {
        anim.Play("settings");
        _inputMenu.SetActive(false);
        _normalMenu.SetActive(false);
        _modeGameMenu.SetActive(false);
        _settingsMenu.SetActive(true);
        _settingsButton.Select();
    }
    public void deactivateSettings()
    {
        anim.Play("backFromSettings");
        _inputMenu.SetActive(false);
        _normalMenu.SetActive(true);
        _modeGameMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _playButton.Select();
    }
    private void Start()
    {
        anim = camera.GetComponent<Animation>();
        StartCoroutine(checkForPressed());
        // NO TOCAR (angulo default de la camara)
        //camera.transform.eulerAngles = startV3;
        AudioManager.Instance._PlayMusic("Menu", true);
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
                    anim.Play("mainMenu");
                    StartCoroutine(SelectButton());
                    break;
                }

            }
            yield return new WaitForSeconds(0.1f);
        }

    }

    private IEnumerator SelectButton()
    {
        yield return new WaitForSeconds(0.2f);
        _playButton.Select();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
