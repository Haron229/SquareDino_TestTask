using UnityEngine;

public interface IPlayerState
{
    void EnterState(GameObject target);
    void ExitState();
}
