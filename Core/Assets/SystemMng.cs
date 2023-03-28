using SampleSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMng : MonoBehaviour, ISystemMng
{
    public float condition = 100;
    protected Statuses status = Statuses.On;

    public float Condition => condition;

    public Statuses Status
    {
        get
        {
            return status;
        }
    }

    public void Damage(Damage D)
    {
        condition -= D.Points;
        if (condition < 0)
        {
            condition = 0;
            status = Statuses.Alert;
        }
    }

    public void Off()
    {
        status = Statuses.Off;
    }

    public void On()
    {
        status = Statuses.On;
    }

    public void Repear(float points)
    {
        condition += points;
    }
}
