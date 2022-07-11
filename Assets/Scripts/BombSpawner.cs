using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject Bomb;
    public float BombDelay = 20f;
    public float Width = 5;
    public float RandomCheck = 1f;
    [SerializeField]
    float chance = 0;

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
        chance = RandomCheck;
        while (true)
        {
            if (prevDelay != BombDelay)
            {
                prevDelay = BombDelay;
                delay = new WaitForSeconds(BombDelay);
            }
            yield return delay;
            if (BombCount >= MaxBombs)
            {
                continue;
            }
            if (UnityEngine.Random.value < chance)
            {
                GameObject go = Instantiate(Bomb, transform.position + new Vector3(Random.value * Width - Width / 2, 0), transform.rotation);
                go.AddComponent<DestroyDetector>().onDestroy = VisitorOnDestroy;
                BombCount++;
                if (chance > RandomCheck)
                {
                    chance = RandomCheck;
                } else
                {
                    chance += -0.5f * RandomCheck + RandomCheck / 2;
                }
            }
            else
            {
                float dif = Mathf.Abs(chance - RandomCheck) / 2;
                chance += Mathf.Max(dif, 0.1f);
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