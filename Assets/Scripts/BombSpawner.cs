using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject Bomb;
    public float BombDelay = 20f;
    public float Width = 5;
    public float RandomCheck = 1f;
    
    void Awake()
    {
        StartCoroutine(SpawnBombs());
    }

    IEnumerator SpawnBombs()
    {
        while (true)
        {
            yield return new WaitForSeconds(BombDelay);
            if (UnityEngine.Random.value < RandomCheck)
            {
                Instantiate(Bomb, transform.position + new Vector3(Random.value * Width - Width / 2, 0), transform.rotation);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 offset = new Vector3( Width/2,0);
        Gizmos.DrawLine(transform.position + offset, transform.position - offset);
    }
}
