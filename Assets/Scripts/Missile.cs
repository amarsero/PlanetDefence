using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Missile : MonoBehaviour
{
    private static List<GameObject> Targeted = new List<GameObject>();

    [SerializeField]
    float rotDegPerSec = 50;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    GameObject Target;
    public MissileState State;
    private Rigidbody2D rigid;
    [SerializeField]
    private float targetAngle;
    [SerializeField]
    private float newRotation;
    [SerializeField]
    private float flightTime;
    public float MaxFlightTime = 5;
    private bool invisible = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public enum MissileState
    {
        StandBy,
        GoingUp,
        FollowingTarget,
    }

    private void Update()
    {
        switch (State)
        {
            case MissileState.GoingUp:
                {
                    rigid.AddForce(new Vector2(0, 400 * Time.deltaTime));
                    if (rigid.velocity.y > (speed / 1.5))
                    {
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
        if (flightTime > MaxFlightTime && invisible)
        {
            DestroySelf();
            return;
        }
    }

    private void FindTarget()
    {
        var possibleTargets = GameObject.FindGameObjectsWithTag("Enemy")
            .Where(x => !Targeted.Contains(x));
        var onlyAliens = possibleTargets.Where(x => x.TryGetComponent(out AlienRandomMovement _));
        if (onlyAliens.Any())
        {
            possibleTargets = onlyAliens;
        }
        else if (!possibleTargets.Any())
        {
            var notNull = Targeted.Where(x => x != null);
            possibleTargets = notNull.Where(x => UnityEngine.Random.value > 0.5f);
            if (!possibleTargets.Any())
            {
                possibleTargets = notNull;
                if (!possibleTargets.Any())
                {
                    return;
                }
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
        DestroySelf();
    }

    private void DestroySelf()
    {
        Targeted.Remove(Target);
        Target = null;

        Destroy(gameObject);
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
        rotDegPerSec += Time.deltaTime * 100;
        flightTime += Time.deltaTime;
        if (Target == null)
        {
            return;
        }
        Vector3 direction = Target.transform.position - transform.position;
        float distance = Mathf.Clamp(direction.sqrMagnitude / 4, 0, 1.6f);
        targetAngle = Vector3.SignedAngle(rigid.velocity, direction + new Vector3(0, -distance), Vector3.forward);
        newRotation = Mathf.Clamp(targetAngle, -rotDegPerSec, rotDegPerSec) * Time.deltaTime * 3;
        rigid.rotation += newRotation;
        rigid.velocity = Quaternion.AngleAxis(newRotation, Vector3.forward) * rigid.velocity;
    }

    private void OnDrawGizmos()
    {
        if (Target != null)
        {
            Vector3 target = Quaternion.AngleAxis(newRotation, Vector3.forward) * rigid.velocity;
            Gizmos.color = Color.green;
            var direction = Target.transform.position - transform.position;
            var distance = Mathf.Clamp(direction.sqrMagnitude, 0, 2);
            Gizmos.DrawLine(transform.position, transform.position + direction + new Vector3(0, -0.8f * distance));
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(newRotation, Vector3.forward) * rigid.velocity);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Target.transform.position);
        }
    }
    private void OnBecameInvisible()
    {
        invisible = true;
    }
    private void OnBecameVisible()
    {
        invisible = false;
    }

}
