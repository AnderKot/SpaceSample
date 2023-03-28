using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleSpace;
using UnityEngine.UIElements;

public class EnergyWeaponMng : MonoBehaviour, IWeaponMng
{
    public LineRenderer WeaponEffector;
    public float condition = 100;
    public float Length = 50;
    public Damage DamageParam = new Damage(10, DamageType.Energy);

    private bool activity = true;
    
    public bool Activity
    {
        get => WeaponEffector.enabled;
        set
        {
            if (value)
                StartFire();
            else
                StopFire();
        }
    }

    public float Condition => condition;

    public void Damage(Damage D)
    {
        if (activity)
        {
            condition -= D.Points;
            if (condition < 0)
            {
                activity = false;
                StopFire();
                condition = 0;
            }
        }   
    }

    public void Off()
    {
        StopFire();
    }

    public void On()
    {
        StartFire();
    }

    public void Repear(float points)
    {
        condition += points;
    }


    public void Fire()
    {
        StartFire();
    }

    public void HoldFire()
    {
        StopFire();
    }

    private void StartFire()
    {
        if(activity)
            WeaponEffector.enabled = true;
    }

    private void StopFire()
    {
        WeaponEffector.enabled = false;
    }

    void Start()
    {
        StopFire();

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
