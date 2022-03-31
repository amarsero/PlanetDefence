using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject Bomb;
    public float BombDelay = 20f;
    public float Width = 5;
    
    void Awake()
    {
        StartCoroutine(SpawnBombs());
    }

    IEnumerator SpawnBombs()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        while (true)
        {
            yield return new WaitForSeconds(BombDelay);
            Instantiate(Bomb, transform.position + new Vector3(Random.value * Width - Width / 2,0), rotation);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 offset = new Vector3( Width/2,0);
        Gizmos.DrawLine(transform.position + offset, transform.position - offset);
    }
}
