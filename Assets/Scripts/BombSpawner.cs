using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject Bomb;
    public float BombDelay = 20f;
    public float Width = 5;
    public float RandomCheck = 1f;

    static public int BombCount = 0;
    static public int MaxBombs = 50;

    void Awake()
    {
        StartCoroutine(SpawnBombs());
    }

    IEnumerator SpawnBombs()
    {
        float prevDelay = BombDelay;
        WaitForSeconds delay = new WaitForSeconds(BombDelay);
        while (true)
        {
            if (prevDelay != BombDelay)
            {
                prevDelay = BombDelay;
                delay = new WaitForSeconds(BombDelay);
            }
            yield return delay;
            if (UnityEngine.Random.value < RandomCheck && BombCount < MaxBombs)
            {
                GameObject go = Instantiate(Bomb, transform.position + new Vector3(Random.value * Width - Width / 2, 0), transform.rotation);
                go.AddComponent<DestroyDetector>().onDestroy = VisitorOnDestroy;
                BombCount++;
            }
        }
    }

    private void VisitorOnDestroy(GameObject destroyed)
    {
        BombCount--;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 offset = new Vector3(Width / 2, 0);
        Gizmos.DrawLine(transform.position + offset, transform.position - offset);
    }
}

public class DestroyDetector : MonoBehaviour
{
    public System.Action<GameObject> onDestroy;

    public DestroyDetector()
    {
    }
    private void OnDestroy()
    {
        onDestroy(gameObject);
    }
}