using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragbombLauncher : MonoBehaviour, IActionable2D, IWeapon
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

    private void OnEnable()
    {
        currentBomb?.SetActive(true);
    }

    private void OnDisable()
    {
        currentBomb?.SetActive(false);
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
        DG.Tweening.DOVirtual.DelayedCall(5, () => CreateBomb());
    }

    private void CreateBomb()
    {
        currentBomb = null;
        currentBomb = Instantiate(Fragbomb, transform.position + Vector3.forward, transform.rotation);
        currentBomb.GetComponent<FragBomb>().enabled = false;
        currentBomb.SetActive(this.isActiveAndEnabled);
    }

    public void WeaponShoot(Vector3 worldPosition)
    {
        CommandShoot();
    }

    public void DoAction()
    {
        CommandShoot();
    }
}
