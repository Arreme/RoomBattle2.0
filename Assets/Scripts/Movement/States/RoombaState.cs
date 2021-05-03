using UnityEngine;

public interface RoombaState
{
    abstract void EnterState(NewRoombaController controller);

    abstract void Stay(NewRoombaController controller);

    abstract Vector2 Exit();
}
