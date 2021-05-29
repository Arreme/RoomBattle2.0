using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class PlayerSetupMenu : MonoBehaviour
{
    private int PlayerIndex;
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

    void Update()
    {
        if (_evenSystem != null)
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
                } else if (button.name == "Skin" && !ReferenceEquals(button, _skinCurrent.gameObject))
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

                } else if (button.name == "Knives" && !ReferenceEquals(button, _knifeCurrent.gameObject)) 
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
        tittleText.SetText("Player " + (PlayerIndex + 1));
        _renderTexture.material = CustomizationManager.Instance.InitializeCamera(PlayerIndex);
        CustomizationManager.Instance.getRoomba(PlayerIndex).SetActive(true);
        SetColor("Blue");
    }

    private void SetHat(string hat)
    {
        PlayerConfigManager.Instance.SetPlayerHat(PlayerIndex,hat);
    }

    private void SetColor(string color)
    {
        Colors colors = (Colors)Enum.Parse(typeof(Colors), color);
        PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, AssetsLoader.Instance.colorGetter(colors), getLightColor(colors));
        switch (colors)
        {
            case Colors.Blue:
                PlayerConfigManager.Instance.SetBlueTeams(PlayerIndex,true);
                break;
            default:
                PlayerConfigManager.Instance.SetBlueTeams(PlayerIndex,false);
                break;
        }
    }

    private Color getLightColor(Colors colorPick)
    {
        Color lightColor = new Color();
        switch (colorPick)
        {
            case Colors.Blue:
                lightColor = new Color(0.01176471f, 0.7450981f, 0.945098f);
                break;
            case Colors.Red:
                lightColor = new Color(1, 0.1169811f, 0.259741f);
                break;
            case Colors.Yellow:
                lightColor = new Color(1, 1, 0);
                break;
            case Colors.Green:
                lightColor = new Color(0, 1, 0);
                break;
            case Colors.Purple:
                lightColor = new Color(0.8186114f, 0.2207547f, 1);
                break;
            case Colors.Orange:
                lightColor = new Color(1, 0.5f, 0);
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
