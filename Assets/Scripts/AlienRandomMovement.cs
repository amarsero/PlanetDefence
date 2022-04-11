using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienRandomMovement : MonoBehaviour, IDamageable
{
    private int targetWaypointIndex;
    private Vector3 targetPosition;
    private Vector3 targetDirection;
    private int prevTargetWaypointIndex = 1;
    Vector3[] waypoints;
    [SerializeField]
    private float Speed = 5;

    public int Health = 3;
    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        SetUpWaypoints();
    }

    private void SetUpWaypoints()
    {
        waypoints = new[]{
            new Vector3(transform.position.x, transform.position.y - 12),
            new Vector3(transform.position.x - 4, transform.position.y - 6),
            new Vector3(transform.position.x + 4, transform.position.y - 6),
            };
        if (UnityEngine.Random.value > 0.4f)
        {
            SetNewTargetWaypoint();
        }
        else
        {
            targetPosition = new Vector3(424, 421);
            targetDirection = -Vector3.down;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - targetPosition).sqrMagnitude < 2)
        {
            SetNewIndex();
            SetNewTargetWaypoint();
        }
        Vector3 perpendicualr = Vector3.Cross(targetDirection, Vector3.forward).normalized;
        transform.position = transform.position + -Time.deltaTime * Speed * targetDirection + 0.01f * Mathf.Sin(Time.time * 3 * Speed) * Speed * perpendicualr;
    }

    private void SetNewTargetWaypoint()
    {
        Vector2 randomPos = UnityEngine.Random.insideUnitCircle * 2;
        targetPosition = waypoints[targetWaypointIndex] + new Vector3(randomPos.x, randomPos.y);
        targetDirection = transform.position - targetPosition;
        targetDirection.Normalize();
    }

    private void SetNewIndex()
    {
        int prev = prevTargetWaypointIndex;
        prevTargetWaypointIndex = targetWaypointIndex;
        targetWaypointIndex = UnityEngine.Random.value > 0.3 ? targetWaypointIndex + Math.Sign(targetWaypointIndex - prev) : prev;
        targetWaypointIndex = Math.Abs(targetWaypointIndex % waypoints.Length);
    }

    public void DoDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(Explosion, transform.position, transform.rotation).transform.localScale = Vector3.one * 2;
        Destroy(gameObject);
    }
}
