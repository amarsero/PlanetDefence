using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject Bomb;
    public float BombDelay = 20f;
    public float Width = 5;
    public float RandomCheck = 1f;

    static public int MaxBombs = 50;
    static public LinkedList<GameObject> Bombs = new LinkedList<GameObject>();

    void Awake()
    {
        StartCoroutine(SpawnBombs());
    }

    IEnumerator SpawnBombs()
    {
        while (true)
        {
            yield return new WaitForSeconds(BombDelay);
            if (UnityEngine.Random.value < RandomCheck && CountBombs() < MaxBombs)
            {
                Bombs.AddFirst(Instantiate(Bomb, transform.position + new Vector3(Random.value * Width - Width / 2, 0), transform.rotation));
            }
        }
    }

    private int CountBombs()
    {
        var current = Bombs.First;
        int count = 0;
        while (current != null)
        {
            if (current.Value == null)
            {
                LinkedListNode<GameObject> toDelete = current;
                current = current.Next;
                Bombs.Remove(toDelete);
            }
            else
            {
                count += 1;
                current = current.Next;
            }
        }
        return count;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 offset = new Vector3(Width / 2, 0);
        Gizmos.DrawLine(transform.position + offset, transform.position - offset);
    }
}
