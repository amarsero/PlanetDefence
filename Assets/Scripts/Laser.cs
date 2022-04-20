using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    private bool _shooting;

    public Transform BulletOrigin;
    [SerializeField] float Energy = 100;
    [SerializeField] float MaxEnergy = 100;
    [SerializeField] bool Reloading = false;
    [SerializeField] float EnergyConsumePerS = 60;
    [SerializeField] float EnergyRechargePerS = 40;
    [SerializeField] float RaySize = 0;
    [SerializeField] float RayMaxSize = 5;
    [SerializeField] float RaySizeChangePerS = 5;
    [SerializeField] float LineMaxWidth = 0.3f;
    [SerializeField] float DamagePerS = 2f;
    private LineRenderer line;
    private Image ring;

    // Start is called before the first frame update
    private void Start()
    {
        PlayerController.Instance.OnShoot += PlayerControllerOnShoot;
        line = GetComponent<LineRenderer>();
        line.endWidth = 0;
        line.startWidth = 0;
        ring = GetComponentInChildren<UnityEngine.UI.Image>();
    }
    private void OnDestroy()
    {
        PlayerController.Instance.OnShoot -= PlayerControllerOnShoot;
    }

    private void Update()
    {
        Energy += EnergyRechargePerS * Time.deltaTime;
        if (Energy > MaxEnergy)
        {
            Energy = MaxEnergy;
            Reloading = false;
        }
        line.endWidth = LineMaxWidth * RaySize / RayMaxSize;
        line.startWidth = LineMaxWidth * RaySize / RayMaxSize;
        if (RaySize > 0)
        {
            RaycastHit2D coll = Physics2D.Raycast(transform.position, transform.localRotation * Vector2.up, 50, LayerMask.GetMask("Enemy"));
            if (coll)
            {
                line.SetPosition(1, new Vector3(0, coll.distance, 0));
                coll.transform.gameObject.GetComponent<IDamageable>()?.DoDamage(Time.deltaTime*DamagePerS * RaySize / RayMaxSize);
            }
            else
            {
                line.SetPosition(1, new Vector3(0, 50, 0));
            }
        }
        RaySize -= RaySizeChangePerS * Time.deltaTime;
        RaySize = Mathf.Clamp(RaySize, 0, RayMaxSize);
        ring.fillAmount = Energy / MaxEnergy;
        ring.color = Color.Lerp(Color.yellow, Color.green, ring.fillAmount);
    }

    private void OnValidate()
    {
        GetComponent<LineRenderer>().endWidth = LineMaxWidth;
        GetComponent<LineRenderer>().startWidth = LineMaxWidth;
    }

    private void PlayerControllerOnShoot(Vector3 worldPosition)
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
}
