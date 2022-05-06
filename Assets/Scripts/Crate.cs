using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IActionable2D
{
    public float Speed = 2;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - Speed * Time.deltaTime, transform.position.z);
    }
    public void DoAction()
    {
        Pickup();
        Destroy(gameObject);
    }

    private void Pickup()
    {
        Debug.Log("wow tienes un poder nuevo, que cool");
    }
}
