using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [SerializeField]
    private GameObject menuPrefab;

    [SerializeField]
    private Material _powerUpOn;

    [SerializeField]
    private GameObject leftPanel;
    [SerializeField]
    private GameObject rightPanel;

    [SerializeField]
    private Color[] baseColor;

    private MenuConfig[] menus = new MenuConfig[6];

    [SerializeField]
    private Sprite[] powerUpImages;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void InitializeMenu(int playerIndex, Colors colorSelected, bool isIA)
    {
        GameObject reference;
        TextMeshProUGUI text;
        if (playerIndex % 2 == 0)
        {
            reference = Instantiate(menuPrefab, leftPanel.transform);
            Transform child = reference.transform.GetChild(0);
            child.localScale = new Vector3(-1,1,1);
            child.localPosition = new Vector3(-child.localPosition.x, child.localPosition.y, child.localPosition.z);
            text = reference.GetComponentInChildren<TextMeshProUGUI>();
            text.transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            reference = Instantiate(menuPrefab,rightPanel.transform);
            text = reference.GetComponentInChildren<TextMeshProUGUI>();
        }
        if (isIA) text.SetText("IA " + (playerIndex + 1));
        else text.SetText("Player " + (playerIndex + 1));

        menus[playerIndex] = new MenuConfig(reference, SetColor(colorSelected), new Material(_powerUpOn),SetColorGlow(colorSelected));
        Image menu = menus[playerIndex].menuPanel.GetComponentInChildren<Image>();
        menu.sprite = powerUpImages[6];
        menu.transform.GetChild(0).GetComponent<Image>().color = menus[playerIndex].color;
    }

    internal void SetPowerUp(int playerIndex, int random)
    {
        Image menu = menus[playerIndex].menuPanel.GetComponentInChildren<Image>();
        menu.sprite = powerUpImages[random - 1];
        menu.transform.GetChild(0).GetComponent<Image>().material = menus[playerIndex].materialHud;
    }

    private Color SetColor(Colors colorSelected)
    {
        switch (colorSelected)
        {
            case Colors.Blue:
                return baseColor[2];
            case Colors.Red:
                return baseColor[0];
            case Colors.Yellow:
                return baseColor[1];
            case Colors.Green:
                return baseColor[4];
            case Colors.Purple:
                return baseColor[3];
            case Colors.Orange:
                return baseColor[5];
            default:
                return baseColor[0];
        }
    }

    private Color SetColorGlow(Colors colorSelected)
    {
        switch (colorSelected)
        {
            case Colors.Blue:
                return new Color(1.69946218f, 4.94389343f, 29.5088463f, 10);
            case Colors.Red:
                return new Color(29.5088463f, 1.62298679f, 1.62298679f, 10);
            case Colors.Yellow:
                return new Color(29.5088463f, 25.3374386f, 1.69946218f, 10);
            case Colors.Green:
                return new Color(6.33436155f, 29.5088463f, 1.69946218f, 10);
            case Colors.Purple:
                return new Color(14.6771755f, 1.69946218f, 29.5088463f, 10);
            case Colors.Orange:
                return new Color(29.5088463f, 18.3850937f, 1.69946218f, 10);
            default:
                return baseColor[0];
        }
        
    }
    public void resetPowerUp(int playerIndex)
    {
        Image menu = menus[playerIndex].menuPanel.GetComponentInChildren<Image>();
        menu.sprite = powerUpImages[6];
        menu.transform.GetChild(0).GetComponent<Image>().material = null;
    }


}


class MenuConfig
{
    public GameObject menuPanel { get; set; }
    public Color color { get; set; }

    public Material materialHud { get; set; }
    public MenuConfig(GameObject menuPanel, Color color, Material mat, Color glow)
    {
        this.menuPanel = menuPanel;
        this.color = color;
        materialHud = mat;
        materialHud.color = color;
        materialHud.SetColor("GlowColor", glow);
    }
}
