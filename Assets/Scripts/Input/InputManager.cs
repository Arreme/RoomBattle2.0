
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerConfig playerConfig;
    //private RoombaController player;

    [SerializeField]
    private MeshRenderer playerMesh;

    private RoombaInputSystem controls;
    
    void Awake()
    {
        //player = GetComponent<RoombaController>();
        controls = new RoombaInputSystem();
    }

    public void InitializePlayer(PlayerConfig conf)
    {
        playerConfig = conf;
        playerMesh.material = playerConfig.PlayerMaterial;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(InputAction.CallbackContext obj)
    {
        if (obj.action.name == controls.InGame.Move.name)
        {
            Debug.Log(obj.ReadValue<Vector2>());
            //player.SetInputVector(obj.ReadValue<Vector2>());
        }
        else if (obj.action.name == controls.InGame.Boost.name)
        {
            Debug.Log(obj.ReadValueAsButton());
            //player.SetBoost(obj.ReadValueAsButton());
        }
        else if (obj.action.name == controls.InGame.Action.name)
        {
            Debug.Log(obj.ReadValueAsButton());
            //player.SetAction(obj.ReadValueAsButton());
        }
    }
}
