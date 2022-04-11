using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Missile : MonoBehaviour
{
    private static List<GameObject> Targeted = new List<GameObject>();

    [SerializeField]
    float rotDegPerSec = 60;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    GameObject Target;
    [SerializeField]
    MissileState State;
    private Rigidbody2D rigid;
    private Vector3 startingPosition;
    [SerializeField]
    private float targetAngle;
    [SerializeField]
    private float newRotation;
    [SerializeField]
    private float flightTime;
    public float MaxFlightTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    enum MissileState
    {
        StandBy,
        GoingUp,
        FollowingTarget,
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            if (Time.timeScale == 0.25f)
            {
                Time.timeScale = 0.5f;
            }
            else if (Time.timeScale == 0.5f)
            {
                Time.timeScale = 0.75f;
            }
            else if (Time.timeScale == 0.75f)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0.25f;
            }
        }
        switch (State)
        {
            case MissileState.GoingUp:
                {
                    rigid.AddForce(new Vector2(0, 400 * Time.deltaTime));
                    if (rigid.velocity.y > speed)
                    {
                        rigid.velocity = new Vector2(0, speed);
                        State = MissileState.FollowingTarget;
                    }
                }
                break;
            case MissileState.FollowingTarget:
                FollowTarget();
                break;
            default:
                break;
        }
    }

    private void FindTarget()
    {
        var possibleTargets = GameObject.FindGameObjectsWithTag("Enemy")
            .Where(x => !Targeted.Contains(x));
        if (!possibleTargets.Any())
        {
            possibleTargets = Targeted.Where(x => x != null);
            if (!possibleTargets.Any())
            {
                return;
            }
        }
        Target = possibleTargets
            .Select(x => ((x.transform.position - transform.position).sqrMagnitude, x))
            .Aggregate((curMin, x) => x.sqrMagnitude <
            curMin.sqrMagnitude ? x : curMin).x;
        Targeted.Add(Target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }
        var damagable = collision.GetComponent<IDamageable>();
        if (damagable != null)
        {
            damagable.DoDamage(3);
        }
        Explode();
    }

    private void Explode()
    {
        Targeted.Remove(Target);
        Target = null;
        transform.position = startingPosition;
        transform.rotation = Quaternion.identity;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
        rigid.rotation = 0;
        flightTime = 0;
        State = MissileState.StandBy;
    }

    public void LaunchMissile()
    {
        State = MissileState.GoingUp;
    }

    public void FollowTarget()
    {
        if (Target == null)
        {
            FindTarget();
        }
        rigid.velocity += rigid.velocity.normalized * Time.deltaTime;
        rotDegPerSec += Time.deltaTime * 3;
        flightTime = Time.deltaTime;
        if (flightTime > MaxFlightTime)
        {
            Explode();
            return;
        }
        if (Target == null)
        {
            return;
        }

        targetAngle = Vector3.SignedAngle(rigid.velocity, Target.transform.position - transform.position, Vector3.forward);
        newRotation = Mathf.Clamp(targetAngle, -rotDegPerSec, rotDegPerSec) * Time.deltaTime;
        rigid.rotation = rigid.rotation + newRotation;
        rigid.velocity = Quaternion.AngleAxis(newRotation, Vector3.forward) * rigid.velocity;
    }

    internal bool ReadyToLaunch()
    {
        return State == MissileState.StandBy;
    }

    private void OnDrawGizmos()
    {
        if (Target != null)
        {
            Vector3 target = Quaternion.AngleAxis(newRotation, Vector3.forward) * rigid.velocity;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + target);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Target.transform.position);
        }
    }

}
