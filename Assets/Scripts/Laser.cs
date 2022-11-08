using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : Weapon
{
    [SerializeField] public float Energy = 100;
    [SerializeField] public float MaxEnergy = 200;
    [SerializeField] bool Reloading = false;
    [SerializeField] float EnergyConsumePerS = 60;
    [SerializeField] float EnergyRechargePerS = 20;
    [SerializeField] float RaySize = 0;
    [SerializeField] float RayMaxSize = 5;
    [SerializeField] float RaySizeChangePerS = 5;
    [SerializeField] float LineMaxWidth = 0.3f;
    [SerializeField] float DamagePerS = 4f;
    private LineRenderer line;
    private Material ringo;
    private ParticleSystem particles;
    private float particleSize = 0.3f;

    // Start is called before the first frame update
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.endWidth = 0;
        line.startWidth = 0;
        ringo = transform.Find("Ringo").GetComponent<SpriteRenderer>().sharedMaterial;
        particles = GetComponentInChildren<ParticleSystem>();
    }

    internal void GiveAmmo()
    {
        Energy = Mathf.Min(Energy + MaxEnergy * 0.5f, MaxEnergy);
    }

    private void Update()
    {
        Energy += EnergyRechargePerS * Time.deltaTime;
        if (Energy > MaxEnergy)
        {
            Energy = MaxEnergy;
            Reloading = false;
        }
        float raySizeProportion = RaySize / RayMaxSize;
        line.endWidth = LineMaxWidth * raySizeProportion;
        line.startWidth = LineMaxWidth * raySizeProportion;
        if (RaySize > 0)
        {
            RaycastHit2D coll = Physics2D.Raycast(transform.position, transform.localRotation * Vector2.up, 50, LayerMask.GetMask("Enemy"));
            if (coll)
            {
                line.SetPosition(1, new Vector3(0, coll.distance, 0));
                coll.transform.gameObject.GetComponent<IDamageable>()?.DoDamage(Time.deltaTime * DamagePerS * raySizeProportion);
                particles.gameObject.transform.position = coll.point;
                var main = particles.main;
                main.startSize = raySizeProportion * particleSize;
                if (!particles.isPlaying)
                {
                    particles.Play();
                }
                particles.time = 0;
            }
            else
            {
                line.SetPosition(1, new Vector3(0, 50, 0));
            }
        }
        RaySize -= RaySizeChangePerS * Time.deltaTime;
        RaySize = Mathf.Clamp(RaySize, 0, RayMaxSize);
        float eneryPerc = Energy / MaxEnergy;
        ringo.SetFloat("_Arc", 1 - eneryPerc);
        ringo.color = Color.Lerp(Color.yellow, Color.green, eneryPerc);
    }

    private void OnValidate()
    {
        GetComponent<LineRenderer>().endWidth = LineMaxWidth;
        GetComponent<LineRenderer>().startWidth = LineMaxWidth;
    }
    override public void WeaponShoot(Vector3 worldPosition)
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, worldPosition - transform.position);
        CommandShoot();
    }

    private void CommandShoot()
    {
        if (Reloading)
        {
            return;
        }
        Energy -= EnergyConsumePerS * Time.deltaTime;
        RaySize += RaySizeChangePerS * Time.deltaTime * 2;
        if (Energy < 0)
        {
            Reloading = true;
            Energy = 0;
        }
    }

    public override bool CanShoot()
    {
        return Energy > 0;
    }
}
