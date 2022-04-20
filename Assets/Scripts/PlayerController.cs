using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnShootDelegate(Vector3 worldPosition);
    public event OnShootDelegate OnShoot;
    private int shootingFingerId = -1;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount <= 0)
        {
            shootingFingerId = -1;
            return;
        }
        if (shootingFingerId != -1)
        {
            int currentShootingFingerId = -1;
            foreach (var touch in Input.touches)
            {
                if (shootingFingerId == touch.fingerId)
                {
                    currentShootingFingerId = touch.fingerId;
                    OnShoot?.Invoke(TouchToWorldPostion(touch.position));
                    break;
                }
            }
            shootingFingerId = currentShootingFingerId;
        }
        foreach (var touch in Input.touches)
        {
            Debug.Log($"Finger id thouched: {touch.fingerId}, state:{touch.phase}");
            if (touch.phase == TouchPhase.Began)
            {
                IActionable2D actionable;
                Vector3 pos = TouchToWorldPostion(touch.position);
                if (null != (actionable = Physics2D.OverlapPoint(new Vector2(pos.x, pos.y))?.GetComponent<IActionable2D>()))
                {
                    actionable.DoAction();
                }
                else if (shootingFingerId == -1)
                {
                    shootingFingerId = touch.fingerId;
                    OnShoot?.Invoke(pos);
                }
            }

        }
    }
    Vector3 TouchToWorldPostion(Vector3 position)
    {
        Vector3 pos = new Vector3(position.x, position.y);
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = 0;
        return pos;
    }
}
