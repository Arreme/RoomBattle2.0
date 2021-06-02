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
    private GameObject leftPanel;
    [SerializeField]
    private GameObject rightPanel;

    [SerializeField]
    private Color[] baseColor;
    [SerializeField]
    private Color[] imageColor;

    private float transparency = 0.3f;

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

    public void InitializeMenu(int playerIndex, Colors colorSelected)
    {
        GameObject reference;
        TextMeshProUGUI text;
        if (playerIndex % 2 == 0)
        {
            reference = Instantiate(menuPrefab, leftPanel.transform);
            reference.transform.localScale = new Vector3(-1, 1, 1);
            text = reference.GetComponentInChildren<TextMeshProUGUI>();
            text.transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            reference = Instantiate(menuPrefab,rightPanel.transform);
            text = reference.GetComponentInChildren<TextMeshProUGUI>();
        }
        text.SetText("Player "+(playerIndex+1));
        menus[playerIndex] = new MenuConfig(reference,colorSelected);
        SetColor(reference, colorSelected);
    }

    internal void SetPowerUp(int playerIndex, int random)
    {
        Image menu = menus[playerIndex].menuPanel.GetComponentInChildren<Image>();
        Color transp = menu.color;
        transp.a = 255;
        menu.color = transp;
        menu.sprite = powerUpImages[random - 1];
    }

    private void SetColor(GameObject menu, Colors colorSelected)
    {
        Image[] images = menu.GetComponentsInChildren<Image>();
        switch (colorSelected)
        {
            case Colors.Blue:
                images[0].color = imageColor[2];
                images[1].color = baseColor[2];
                break;
            case Colors.Red:
                images[0].color = imageColor[0];
                images[1].color = baseColor[0];
                break;
            case Colors.Yellow:
                images[0].color = imageColor[1];
                images[1].color = baseColor[1];
                break;
            case Colors.Green:
                images[0].color = imageColor[4];
                images[1].color = baseColor[4];
                break;
            case Colors.Purple:
                images[0].color = imageColor[3];
                images[1].color = baseColor[3];
                break;
            case Colors.Orange:
                images[0].color = imageColor[5];
                images[1].color = baseColor[5];
                break;
        }
        Color transp = images[0].color;
        transp.a = transparency;
        images[0].color = transp;
    }

    public void resetPowerUp(int playerIndex)
    {
        Image menu = menus[playerIndex].menuPanel.GetComponentInChildren<Image>();
        Color transp = menu.color;
        transp.a = transparency;
        menu.color = transp;
        menu.sprite = null;
    }


}


class MenuConfig
{
    public GameObject menuPanel { get; set; }
    public Colors color { get; set; }
    public MenuConfig(GameObject menuPanel, Colors color)
    {
        this.menuPanel = menuPanel;
        this.color = color;
    }
}
