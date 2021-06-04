using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public GameObject playerSetupMenuPrefab;
    public PlayerInput input;

    private GameObject rootMenu;
    private void Awake()
    {
        rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null)
        {
            var menu = Instantiate(playerSetupMenuPrefab,rootMenu.transform);
            menu.transform.SetAsFirstSibling();
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetupMenu>().SetPlayerIndex(input.playerIndex);

        }
    }
}
