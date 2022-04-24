using UnityEngine;
using UnityEngine.AI;

public class PlayerIdleState : IPlayerState
{
    private InputManager inputManager;
    private NavMeshAgent agent;
    private Pooler bulletPool;
    private Transform bulletSpawnPoint;
    private Camera cameraMain;
    private Animator animator;

    public PlayerIdleState(InputManager inputManager, NavMeshAgent agent, Pooler pool, Transform bulletSpawnPoint, Camera cameraMain, Animator animator)
    {
        this.inputManager = inputManager;
        this.agent = agent;
        bulletPool = pool;
        this.bulletSpawnPoint = bulletSpawnPoint;
        this.cameraMain = cameraMain;
        this.animator = animator;
    }
    public void EnterState(GameObject target)
    {
        inputManager.TouchAction += Shoot;

        Debug.Log("Player entered Idle state");

        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - agent.transform.position);
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 0.5f);
    }

    public void ExitState()
    {
        inputManager.TouchAction -= Shoot;

        Debug.Log("Player exit Idle state");
    }

    private void Shoot(Vector2 position)
    {
        GameObject bullet = bulletPool.GetPoolObject();
        bullet.transform.position = bulletSpawnPoint.position;

        RaycastHit hit;
        Ray ray = cameraMain.ScreenPointToRay(position);
        Physics.Raycast(ray, out hit);

        bullet.transform.forward = hit.point - bullet.transform.position;

        bullet.SetActive(true);
    }
}
