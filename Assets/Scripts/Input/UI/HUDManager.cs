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
            reference.transform.localScale = new Vector3(-1, 1, 1);
            text = reference.GetComponentInChildren<TextMeshProUGUI>();
            text.transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            reference = Instantiate(menuPrefab,rightPanel.transform);
            text = reference.GetComponentInChildren<TextMeshProUGUI>();
        }
        if (isIA) text.SetText("IA " + (playerIndex + 1));
        else text.SetText("Player " + (playerIndex + 1));

        menus[playerIndex] = new MenuConfig(reference,colorSelected);
        SetColor(reference, colorSelected);
        Image menu = menus[playerIndex].menuPanel.GetComponentInChildren<Image>();
        menu.sprite = powerUpImages[6];
    }

    internal void SetPowerUp(int playerIndex, int random)
    {
        Image menu = menus[playerIndex].menuPanel.GetComponentInChildren<Image>();
        menu.sprite = powerUpImages[random - 1];
        menu.material = _powerUpOn;
    }

    private void SetColor(GameObject menu, Colors colorSelected)
    {
        Image[] images = menu.GetComponentsInChildren<Image>();
        switch (colorSelected)
        {
            case Colors.Blue:
                images[1].color = baseColor[2];
                break;
            case Colors.Red:
                images[1].color = baseColor[0];
                break;
            case Colors.Yellow:
                images[1].color = baseColor[1];
                break;
            case Colors.Green:
                images[1].color = baseColor[4];
                break;
            case Colors.Purple:
                images[1].color = baseColor[3];
                break;
            case Colors.Orange:
                images[1].color = baseColor[5];
                break;
        }
    }

    public void resetPowerUp(int playerIndex)
    {
        Image menu = menus[playerIndex].menuPanel.GetComponentInChildren<Image>();
        menu.sprite = powerUpImages[6];
        menu.material = null;
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
