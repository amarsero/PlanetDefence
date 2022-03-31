using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragbombLauncher : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CommandShoot();
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
        currentBomb = Instantiate(Fragbomb, transform.position, transform.rotation);
        currentBomb.GetComponent<FragBomb>().enabled = false;
    }
    
}
