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
    private GameObject _colorCurrent;
    [SerializeField]
    private GameObject _skinCurrent;
    [SerializeField]
    private GameObject _knifeCurrent;
    [SerializeField]
    private GameObject _readyButton;

    [SerializeField]
    private Image _renderTexture;

    void Update()
    {
        if (_evenSystem != null)
        {
            GameObject button = _evenSystem.currentSelectedGameObject;
            if (button != null)
            {
                if (button.name == "Color" && !ReferenceEquals(button, _colorCurrent))
                {
                    _colorCurrent.transform.localScale = new Vector3(0, 0, 0);
                    _colorCurrent = button;
                    _colorCurrent.transform.localScale = new Vector3(1, 1, 1);
                    Button realButton = button.GetComponent<Button>();
                    SetColor(realButton.GetComponentInChildren<TextMeshProUGUI>().text);
                    Button skin = _skinCurrent.GetComponent<Button>();
                    var skinNav = skin.navigation;
                    skinNav.selectOnUp = realButton;
                    skin.navigation = skinNav;
                }
            }
        }

    }

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        tittleText.SetText("Player " + (PlayerIndex + 1));
        _renderTexture.material = CustomizationManager.Instance.InitializeCamera(PlayerIndex);
        CustomizationManager.Instance.getRoomba(PlayerIndex).SetActive(true);
        PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, AssetsLoader.Instance.colorGetter(Colors.Blue), getLightColor(Colors.Blue));
    }

    private void SetColor(string color)
    {
        Colors colors = (Colors)Enum.Parse(typeof(Colors), color);
        PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, AssetsLoader.Instance.colorGetter(colors), getLightColor(colors));
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
                lightColor = new Color(241 / 255, 33 / 255, 3 / 255);
                break;
            case Colors.Yellow:
                lightColor = new Color(1, 1, 0);
                break;
            case Colors.Green:
                lightColor = new Color(0, 1, 0);
                break;
            case Colors.Purple:
                lightColor = new Color(165 / 255, 0, 1);
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
