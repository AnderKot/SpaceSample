using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleSpace;
using UnityEngine.UIElements;

public class EnergyWeaponMng : SystemMng, IWeaponMng
{
    public LineRenderer WeaponEffector;
    public float Length = 50;
    public Damage DamageParam = new Damage(10, DamageType.Energy);

    private bool activity = true;
    
    public bool Activity
    {
        get => WeaponEffector.enabled;
        set
        {
            if (value)
                Fire();
            else
                HoldFire();
        }
    }

    new public void Damage(Damage D)
    {
        base.Damage(D);
        if (status == Statuses.Alert)
            HoldFire();
    }

    new public void Off()
    {
        base.Off();
        Fire();
    }

    new public void On()
    {
        base.On();
        HoldFire();
    }

    public void Fire()
    {

        if (activity)
            WeaponEffector.enabled = true;
    }

    public void HoldFire()
    {
        WeaponEffector.enabled = false;
    }

    void Start()
    {
        HoldFire();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (WeaponEffector.enabled)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, Length))
            {
                WeaponEffector.SetPosition(1, Vector3.forward * Vector3.Distance(transform.position, hit.point));

                ISystemMng sys = hit.collider.transform.GetComponent<ISystemMng>();
                if (sys != null)
                {
                    sys.Damage(DamageParam);
                    Debug.Log("Hit&Damage_" + hit.collider.transform.name);

                }

                sys = hit.collider.transform.parent.GetComponent<ISystemMng>();
                if (sys != null)
                {
                    sys.Damage(DamageParam);
                    Debug.Log("Hit&Damage_Parent_" + hit.collider.transform.name);

                }

                Debug.Log("Hit_" + hit.collider.transform.name);
            }
            else
            WeaponEffector.SetPosition(1, Vector3.forward * Length);
        }
    }
}
