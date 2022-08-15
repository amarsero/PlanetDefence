using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private UIButton button;

    public void Select()
    {
        gameObject.SetActive(true);
        this.button.Renderer.sprite = this.button.Seleccionado;
    }

    public void Deselect()
    {
        gameObject.SetActive(false);
        this.button.Renderer.sprite = CanShoot() ? this.button.ConMunicion : this.button.SinMunicion;
    }

    public abstract void WeaponShoot(Vector3 worldPosition);
    public abstract bool CanShoot();

    internal void SetButton(UIButton button)
    {
        this.button = button;
    }
}
