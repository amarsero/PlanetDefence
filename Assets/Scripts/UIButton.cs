using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour, IActionable2D
{
    public GameObject Weapon;
    private WeaponSelector weaponSelector;
    private IWeapon weaponScript;

    private void Start()
    {
        weaponSelector = transform.parent.parent.GetComponentInChildren<WeaponSelector>();
        weaponScript = Weapon.GetComponent<IWeapon>();
    }
    public void DoAction()
    {
        weaponSelector.SwitchToWeapon(weaponScript);
    }
}
