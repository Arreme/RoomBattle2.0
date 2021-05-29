using System.Collections.Generic;
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
    [SerializeField]
    private GameObject parentKnife;
    [SerializeField]
    private GameObject parentBody;

    private RoombaInputSystem controls;

    public bool _teamBlue;
    
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
                BattleManager.Instance._blueAlive += 1;
            } else
            {
                BattleManager.Instance._redAlive += 1;
            }
            _teamBlue = conf.TeamBlue;
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
        if (conf.hatInstance != null)
        {
            Instantiate(conf.hatInstance, parentBody.transform);
        }
        Debug.Log(conf.kniveInstance);
        if (conf.kniveInstance != null)
        {
            parentKnife.GetComponent<MeshFilter>().sharedMesh = conf.kniveInstance.GetComponent<MeshFilter>().sharedMesh;
            parentKnife.transform.localPosition = conf.kniveInstance.transform.localPosition;
            parentKnife.transform.localRotation = conf.kniveInstance.transform.localRotation;
            parentKnife.transform.localScale = conf.kniveInstance.transform.localScale;
        }
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
