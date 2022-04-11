using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragbombLauncher : MonoBehaviour, IActionable2D
{
    public GameObject Fragbomb;
    private GameObject currentBomb;

    // Start is called before the first frame update
    void Start()
    {
        if (currentBomb == null)
        {
            CreateBomb();
        }
    }

    public void CommandShoot()
    {
        if (currentBomb == null)
        {
            return;
        }
        var bomb = currentBomb;
        currentBomb = null;
        bomb.GetComponent<FragBomb>().enabled = true;
        StartCoroutine(CoroutineHelper.WaitSecondsAnd(5, () => CreateBomb()));
    }
    private void CreateBomb()
    {
        currentBomb = null;
        currentBomb = Instantiate(Fragbomb, transform.position + Vector3.forward, transform.rotation);
        currentBomb.GetComponent<FragBomb>().enabled = false;
    }

    public void DoAction()
    {
        CommandShoot();
    }
}
