using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour, IActionable2D
{
    public GameObject Weapon;
    private WeaponSelector weaponSelector;
    private Weapon weaponScript;
    public Sprite Seleccionado;
    public Sprite ConMunicion;
    public Sprite SinMunicion;
    public SpriteRenderer Renderer;

    private void Awake()
    {
        weaponSelector = transform.parent.parent.GetComponentInChildren<WeaponSelector>();
        weaponScript = Weapon.GetComponent<Weapon>();
        weaponScript.SetButton(this);
        Renderer = GetComponent<SpriteRenderer>();
    }
    public void DoAction()
    {
        weaponSelector.SwitchToWeapon(weaponScript);
    }
}
