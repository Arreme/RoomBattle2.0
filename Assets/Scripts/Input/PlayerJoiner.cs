using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerJoiner : MonoBehaviour
{
    [SerializeField] private PlayerInputManager _manager;
    private List<int> devideId = new List<int>();

    private int _id;
    private bool _firstKeyboard = false;
    private bool _secondKeyboard = false;

    void Update()
    {
        if (_manager.playerCount >= _manager.maxPlayerCount)
        {
            return;
        }

        if (!_firstKeyboard && Keyboard.current.shiftKey.isPressed)
        {
            _manager.JoinPlayer(_id, -1, "Keyboard1", Keyboard.current);
            _id++;
            _firstKeyboard = true;
        }
        else if (!_secondKeyboard && Keyboard.current.enterKey.isPressed)
        {
            _manager.JoinPlayer(_id, -1, "Keyboard2", Keyboard.current);
            _id++;
            _secondKeyboard = true;
        }
        else if (Gamepad.all.Count >= 1 && Gamepad.current.buttonSouth.isPressed && devideId.All(n => !n.Equals(Gamepad.current.deviceId)))
        {
            devideId.Add(Gamepad.current.deviceId);
            _manager.JoinPlayer(_id, -1, "Gamepad", Gamepad.current);
            _id++;
        }
    }
}
