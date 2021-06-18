using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerJoiner : MonoBehaviour
{
    [SerializeField] private PlayerInputManager _manager;
    private List<int> devideId = new List<int>();

    [SerializeField]
    private GameObject _shiftJoin;

    [SerializeField]
    private GameObject _enterJoin;

    [SerializeField]
    private GameObject _aJoin;

    [SerializeField]
    private GameObject _xJoin;

    private int _id;
    private bool _firstKeyboard = false;
    private bool _secondKeyboard = false;
    private bool inputDelay = true;

    private void Awake()
    {
        StartCoroutine(delay());
    }
    private IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        inputDelay = false;
    }

    void Update()
    {
        if (_manager.playerCount >= _manager.maxPlayerCount)
        {
            if (_aJoin != null) Destroy(_aJoin);
            if (_shiftJoin != null) Destroy(_shiftJoin);
            if (_enterJoin != null) Destroy(_enterJoin);
            if (_xJoin != null) Destroy(_xJoin);
            return;
        }

        if (inputDelay)
        {
            return;
        }

        if (!_firstKeyboard && Keyboard.current.shiftKey.isPressed)
        {
            if (_shiftJoin != null) Destroy(_shiftJoin);
            _manager.JoinPlayer(_id, -1, "Keyboard1", Keyboard.current);
            _id++;
            _firstKeyboard = true;
        }
        else if (!_secondKeyboard && Keyboard.current.enterKey.isPressed)
        {
            if (_enterJoin != null) Destroy(_enterJoin);
            _manager.JoinPlayer(_id, -1, "Keyboard2", Keyboard.current);
            _id++;
            _secondKeyboard = true;
        }
        else if (Gamepad.all.Count >= 1 && Gamepad.current.buttonSouth.isPressed && devideId.All(n => !n.Equals(Gamepad.current.deviceId)))
        {
            devideId.Add(Gamepad.current.deviceId);
            _manager.JoinPlayer(_id, -1, "Gamepad", Gamepad.current);
            _id++;
        } else if (_id >= 1 && (Keyboard.current.xKey.wasPressedThisFrame || Gamepad.all.Count >= 1 && Gamepad.current.buttonWest.wasPressedThisFrame))
        {
            _manager.JoinPlayer(_id, -1, "IA");
            _id++;

        }
    }
}
