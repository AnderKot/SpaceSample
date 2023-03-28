using SampleSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMng : MonoBehaviour, ISystemMng
{
    protected float condition = 100;
    public float BrokeLine = 10;
    public float MaxCondition = 100;
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
        if (Status != Statuses.Broke)
        {
            condition -= D.Points;
            if (condition < BrokeLine)
            {
                status = Statuses.Broke;
            }
            if (condition < 0)
                condition = 0;
        }

    }

    public void Off()
    {
        if (Status != Statuses.Broke)
            status = Statuses.Off;
    }

    public void On()
    {
        if(Status != Statuses.Broke)
            status = Statuses.On;
    }

    public void Repear(float points)
    {
        condition += points;

        if (condition > BrokeLine)
            status = Statuses.Off;

        if(condition > MaxCondition)
            condition = MaxCondition;
    }
}
