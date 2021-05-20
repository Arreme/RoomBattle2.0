using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSetupMenu : MonoBehaviour
{
    private int PlayerIndex;

    [SerializeField]
    private TextMeshProUGUI tittleText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        tittleText.SetText("Player " + (pi + 1));
        ignoreInputTime = Time.time + ignoreInputTime;
    }
    private void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SetColor(string color)
    {
        if (!inputEnabled) { return; }
        Colors colors = (Colors)Enum.Parse(typeof(Colors),color);
        PlayerConfigManager.Instance.SetPlayerColor(PlayerIndex, AssetsLoader.Instance.colorGetter(colors),getLightColor(colors));
        readyPanel.SetActive(true);
        readyButton.interactable = true;
        menuPanel.SetActive(false);
        readyButton.Select();
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
                lightColor = new Color(241/255, 33/255, 3/255);
                break;
            case Colors.Yellow:
                lightColor = new Color(1, 1, 0);
                break;
            case Colors.Green:
                lightColor = new Color(0, 1, 0);
                break;
            case Colors.Purple:
                lightColor = new Color(165/255, 0, 1);
                break;
            case Colors.Orange:
                lightColor = new Color(1, 0.5f, 0);
                break;
        }
        return lightColor;
    }


    public void ReadyPlayer()
    {
        if (!inputEnabled) return;

        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);
    }
}

public enum Colors
{
    Blue, Yellow, Green, Purple, Red, Orange
}
