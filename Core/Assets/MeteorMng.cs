using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleSpace;

public class MeteorMng : MonoBehaviour, ISystemMng
{
    public float Size = 1;
    public float MinSize = 0.2f;
    public float MaxSize = 3;
    public float GrowthSpeed = 0.005f;
    public float DamageResist = 20;

    public float Condition => throw new System.NotImplementedException();

    public Statuses Status => throw new System.NotImplementedException();

    public void Damage(Damage d)
    {
        Size -= d.Points / DamageResist;
        if (Size <= MinSize)
            Destroy(gameObject);
        transform.localScale = Vector3.one * Size;
    }

    public void Off()
    {
        throw new System.NotImplementedException();
    }

    public void On()
    {
        throw new System.NotImplementedException();
    }

    public void Repear(float poin)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        transform.localScale = Vector3.one * Size;
    }

    void FixedUpdate()
    {
        if (Size < MaxSize)
        {
            Size += GrowthSpeed;
            transform.localScale = Vector3.one * Size;
        }
    }
}
