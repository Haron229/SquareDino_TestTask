using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    private NavMeshAgent agent;
    private Animator animator;

    public Camera cameraMain;

    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private int nextWaypoint;

    private Dictionary<Type, IPlayerState> playerStates;
    private IPlayerState currentState;

    public Transform bulletSpawnPoint;
    [SerializeField]
    private Pooler bulletPool;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        InitPlayerStates();

        if (currentWaypoint < waypoints.Length)
            nextWaypoint = currentWaypoint + 1;

        SetPlayerIdleState();
    }

    void Update()
    {
        if (currentWaypoint == waypoints.Length - 1 && currentState == GetPlayerState<PlayerMovingToWaypointState>() && agent.isStopped)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else if (currentState == GetPlayerState<PlayerMovingToWaypointState>() && agent.isStopped)
            SetPlayerIdleState();
    }

    private void InitPlayerStates()
    {
        playerStates = new Dictionary<Type, IPlayerState>();

        playerStates[typeof(PlayerIdleState)] = new PlayerIdleState(inputManager, agent, bulletPool, bulletSpawnPoint, cameraMain, animator);
        playerStates[typeof(PlayerMovingToWaypointState)] = new PlayerMovingToWaypointState(agent, animator);
    }

    private void SetPlayerState(IPlayerState state, GameObject target)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = state;
        currentState.EnterState(target);
    }

    private IPlayerState GetPlayerState<T>() where T : IPlayerState
    {
        Type type = typeof(T);
        return playerStates[type];
    }

    private void SetPlayerIdleState()
    {
        SetPlayerState(GetPlayerState<PlayerIdleState>(), waypoints[nextWaypoint]);
        waypoints[nextWaypoint].GetComponent<Waypoint>().MoveToWaypointAction += SetPlayerMovingToWaypointState;
    }

    private void SetPlayerMovingToWaypointState()
    {
        currentWaypoint++;
        nextWaypoint++;

        SetPlayerState(GetPlayerState<PlayerMovingToWaypointState>(), waypoints[nextWaypoint]);
        waypoints[nextWaypoint].GetComponent<Waypoint>().MoveToWaypointAction -= SetPlayerMovingToWaypointState;
    }
}
