using UnityEngine;
using UnityEngine.AI;

public class PlayerMovingToWaypointState : IPlayerState
{
    private NavMeshAgent agent;
    private Animator animator;

    public PlayerMovingToWaypointState(NavMeshAgent agent, Animator animator)
    {
        this.agent = agent;
        this.animator = animator;
    }
    public void EnterState(GameObject target)
    {
        Debug.Log("Player entered MovingToWaypoint state");

        animator.SetTrigger("Run");

        agent.SetDestination(target.transform.position);
    }

    public void ExitState()
    {
        Debug.Log("Player exit MovingToWaypoint state");

        animator.ResetTrigger("Run");
    }
}
