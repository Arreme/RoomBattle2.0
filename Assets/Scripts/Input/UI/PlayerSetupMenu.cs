using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class PlayerSetupMenu : MonoBehaviour
{
    private int PlayerIndex;
    private bool isAnIA;
    [SerializeField]
    private TextMeshProUGUI tittleText;
    [SerializeField]
    private MultiplayerEventSystem _evenSystem;

    [SerializeField]
    private Button _colorCurrent;
    [SerializeField]
    private Button _skinCurrent;
    [SerializeField]
    private Button _knifeCurrent;
    [SerializeField]
    private Button _readyButton;

    [SerializeField]
    private Image _renderTexture;

    private static int blueTeamCount = 0;
    private static int redTeamCount = 0;

    void Update()
    {
        if (_evenSystem != null && !isAnIA)
        {
            GameObject button = _evenSystem.currentSelectedGameObject;
            if (button != null)
            {
                if (button.name == "Color" && !ReferenceEquals(button, _colorCurrent.gameObject))
                {
                    _colorCurrent.transform.localScale = new Vector3(0, 0, 0);
                    _colorCurrent = button.GetComponent<Button>();
                    _colorCurrent.transform.localScale = new Vector3(1, 1, 1);

                    SetColor(_colorCurrent.GetComponentInChildren<TextMeshProUGUI>().text);

                    var skinNav = _skinCurrent.navigation;
                    skinNav.selectOnUp = _colorCurrent;
                    _skinCurrent.navigation = skinNav;

                    var currentNav = _colorCurrent.navigation;
                    currentNav.selectOnDown = _skinCurrent;
                    _colorCurrent.navigation = currentNav;
                }
                else if (button.name == "Skin" && !ReferenceEquals(button, _skinCurrent.gameObject))
                {
                    _skinCurrent.transform.localScale = new Vector3(0, 0, 0);
                    _skinCurrent = button.GetComponent<Button>();
                    _skinCurrent.transform.localScale = new Vector3(1, 1, 1);

                    SetHat(_skinCurrent.GetComponentInChildren<TextMeshProUGUI>().text);

                    var knifeNav = _knifeCurrent.navigation;
                    knifeNav.selectOnUp = _skinCurrent;
                    _knifeCurrent.navigation = knifeNav;

                    var colorNav = _colorCurrent.navigation;
                    colorNav.selectOnDown = _skinCurrent;
                    _colorCurrent.navigation = colorNav;

                    var currentNav1 = _skinCurrent.navigation;
                    currentNav1.selectOnUp = _colorCurrent;
                    currentNav1.selectOnDown = _knifeCurrent;
                    _skinCurrent.navigation = currentNav1;

                }
                else if (button.name == "Knives" && !ReferenceEquals(button, _knifeCurrent.gameObject))
                {
                    _knifeCurrent.transform.localScale = new Vector3(0, 0, 0);
                    _knifeCurrent = button.GetComponent<Button>();
                    _knifeCurrent.transform.localScale = new Vector3(1, 1, 1);

                    SetKnive(_knifeCurrent.GetComponentInChildren<TextMeshProUGUI>().text);

                    var kniveNav = _knifeCurrent.navigation;
                    kniveNav.selectOnUp = _skinCurrent;
                    _knifeCurrent.navigation = kniveNav;

                    var skinNav = _skinCurrent.navigation;
                    skinNav.selectOnDown = _knifeCurrent;
                    _skinCurrent.navigation = skinNav;

                    var readyNav = _readyButton.navigation;
                    readyNav.selectOnUp = _knifeCurrent;
                    _readyButton.navigation = readyNav;

                }
            }
        }

    }

    private void SetKnive(string knive)
    {
        PlayerConfigManager.Instance.SetPlayerKnive(PlayerIndex, knive);
    }

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        isAnIA = PlayerConfigManager.Instance.GetPlayerConfigs()[PlayerIndex].IsIA;

        _renderTexture.material = CustomizationManager.Instance.InitializeCamera(PlayerIndex);
        CustomizationManager.Instance.getRoomba(PlayerIndex).SetActive(true);
        if (isAnIA)
        {
            tittleText.SetText("IA " + (PlayerIndex + 1));
            if (PlayerConfigManager.Instance._teamsEnabled)
            {
                if (blueTeamCount == redTeamCount)
                {
                    if (UnityEngine.Random.value >= 0.5)
                    {
                        SetColor("Red");
                    }
                    else
                    {
                        SetColor("Blue");
                    }
                }
                else
                {
                    if (blueTeamCount > redTeamCount || blueTeamCount >= 3)
                    {
                        SetColor("Red");
                    }
                    else if (blueTeamCount < redTeamCount || redTeamCount >= 3)
                    {
                        SetColor("Blue");
                    }
                }
            }
            else
            {
                Array values = Enum.GetValues(typeof(Colors));
                System.Random random = new System.Random();
                Colors randomColor = (Colors)values.GetValue(random.Next(values.Length));
                SetColor(randomColor.ToString());
            }
            _colorCurrent.GetComponentInParent<Image>().gameObject.SetActive(false);
            _knifeCurrent.GetComponentInParent<Image>().gameObject.SetActive(false);
            _skinCurrent.GetComponentInParent<Image>().gameObject.SetActive(false);
            ReadyPlayer();
        }
        else
        {
            tittleText.SetText("Player " + (PlayerIndex + 1));
            SetColor("Blue");
        }

    }

    private void SetHat(string hat)
    {
        PlayerConfigManager.Instance.SetPlayerHat(PlayerIndex, hat);
    }

    private void SetColor(string color)
    {
        Colors colors = (Colors)Enum.Parse(typeof(Colors), color);
        PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, AssetsLoader.Instance.colorGetter(colors), getLightColor(colors), colors);

        switch (colors)
        {
            case Colors.Blue:
                PlayerConfigManager.Instance.SetBlueTeams(PlayerIndex, true);
                blueTeamCount++;
                if (!isAnIA && redTeamCount > 0)
                    redTeamCount--;
                break;
            default:
                PlayerConfigManager.Instance.SetBlueTeams(PlayerIndex, false);
                redTeamCount++;
                if (!isAnIA && blueTeamCount > 0)
                    blueTeamCount--;
                break;
        }
    }

    private Color getLightColor(Colors colorPick)
    {
        Color lightColor = new Color();
        switch (colorPick)
        {
            case Colors.Blue:
                lightColor = new Color(0, 2.81495047f, 31.6267948f, 1f);
                break;
            case Colors.Red:
                lightColor = new Color(33.896759f, 0.532409787f, 0.532409787f, 1);
                break;
            case Colors.Yellow:
                lightColor = new Color(31.6267948f, 31.6267948f, 3.64287686f, 1);
                break;
            case Colors.Green:
                lightColor = new Color(1.59722948f, 33.896759f, 1.59722948f, 1);
                break;
            case Colors.Purple:
                lightColor = new Color(18.074604f, 0.831188262f, 37.7813644f, 10);
                break;
            case Colors.Orange:
                lightColor = new Color(34.1174774f, 10.0030298f, 1.9648807f, 10);
                break;
        }
        return lightColor;
    }


    public void ReadyPlayer()
    {
        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex);
        _colorCurrent.GetComponent<Button>().interactable = false;
        _skinCurrent.GetComponent<Button>().interactable = false;
        _knifeCurrent.GetComponent<Button>().interactable = false;
    }
}

public enum Colors
{
    Blue, Yellow, Green, Purple, Red, Orange
}