﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private NewRoombaController player;

    [SerializeField]
    private MeshRenderer playerMesh;
    [SerializeField]
    private List<MeshRenderer> _balloons;
    [SerializeField]
    private Light _light;

    private RoombaInputSystem controls;
    
    void Awake()
    {
        player = GetComponent<NewRoombaController>();
        controls = new RoombaInputSystem();
    }

    public void InitializePlayer(PlayerConfig conf)
    {
        InitializeGraphics(conf);
        if (PlayerConfigManager.Instance._teamsEnabled)
        {
            if (conf.TeamBlue == true)
            {

            } else
            {

            }
        }
        conf.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void InitializeGraphics(PlayerConfig conf)
    {
        playerMesh.material = conf.PlayerMaterial;
        foreach (MeshRenderer mesh in _balloons)
        {
            mesh.material = conf.ballonMat;
        }
        _light.color = conf.lightColor;
    }

    private void Input_onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == controls.InGame.Control.name)
        {
            player.updateMovement(obj.ReadValue<Vector2>());
        }
        else if (obj.action.name == controls.InGame.Boost.name)
        {
            player.updateBoost(obj.ReadValueAsButton());
        }
        else if (obj.action.name == controls.InGame.Action.name)
        {
            player.updateAction(obj.ReadValueAsButton());
        }
    }
}
