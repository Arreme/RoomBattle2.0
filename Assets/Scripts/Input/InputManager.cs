
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerConfig playerConfig;
    private NewRoombaController player;

    [SerializeField]
    private MeshRenderer playerMesh;

    private RoombaInputSystem controls;
    
    void Awake()
    {
        player = GetComponent<NewRoombaController>();
        controls = new RoombaInputSystem();
    }

    public void InitializePlayer(PlayerConfig conf)
    {
        playerConfig = conf;
        playerMesh.material = conf.PlayerMaterial;
        conf.Input.onActionTriggered += Input_onActionTriggered;
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
