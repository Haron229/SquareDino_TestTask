using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public event OnEnemiesDied MoveToWaypointAction;
    public delegate void OnEnemiesDied();

    public List<GameObject> enemies;

    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
            MoveToWaypointAction?.Invoke();
    }
}
