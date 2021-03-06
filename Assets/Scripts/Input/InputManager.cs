using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private NewRoombaController player;

    [SerializeField]
    private MeshRenderer playerMesh;
    [SerializeField]
    private List<MeshRenderer> _balloons;
    [SerializeField]
    private GameObject _powerUp;
    [SerializeField]
    private GameObject parentKnife;
    [SerializeField]
    private GameObject parentBody;
    [SerializeField]
    private Image _image;

    private RoombaInputSystem controls;

    public bool _teamBlue;

    public PlayerConfig _playerConfig;
    
    void Awake()
    {
        player = GetComponent<NewRoombaController>();
        controls = new RoombaInputSystem();
    }

    public void InitializePlayer(PlayerConfig conf)
    {
        _playerConfig = conf;
        InitializeGraphics(conf);
        if (PlayerConfigManager.Instance._endScreen) return;
        if (PlayerConfigManager.Instance._teamsEnabled)
        {
            if (conf.TeamBlue == true)
            {
                BattleManager.Instance._blueAlive += 1;
            } else
            {
                BattleManager.Instance._redAlive += 1;
            }
            _teamBlue = conf.TeamBlue;
        }
        if (conf.IsIA)
        {
            GetComponent<EnemyAI>().enabled = true;
        }
        GetComponent<PlayerVariables>().PlayerIndex = conf.PlayerIndex;
        conf.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void InitializeGraphics(PlayerConfig conf)
    {
        playerMesh.material = conf.PlayerMaterial;
        foreach (MeshRenderer mesh in _balloons)
        {
            mesh.material = conf.ballonMat;
        }
        if (!PlayerConfigManager.Instance._endScreen) _powerUp.GetComponent<MeshRenderer>().material.color = conf.lightColor;
        if (conf.hatInstance != null)
        {
            Instantiate(conf.hatInstance, parentBody.transform);
        }
        if (conf.kniveInstance != null)
        {
            parentKnife.GetComponent<MeshFilter>().sharedMesh = conf.kniveInstance.GetComponent<MeshFilter>().sharedMesh;
            parentKnife.GetComponent<MeshRenderer>().sharedMaterial = conf.kniveInstance.GetComponent<MeshRenderer>().sharedMaterial;
            parentKnife.transform.localPosition = conf.kniveInstance.transform.localPosition;
            parentKnife.transform.localRotation = conf.kniveInstance.transform.localRotation;
            parentKnife.transform.localScale = conf.kniveInstance.transform.localScale;
        }
        if (!PlayerConfigManager.Instance._endScreen) HUDManager.Instance.InitializeMenu(conf.PlayerIndex, conf.colorSelected,conf.IsIA,_image);
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
