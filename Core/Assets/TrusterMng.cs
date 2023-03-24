using UnityEngine;
using SampleSpace;

public class TrusterMng : MonoBehaviour, ITrusterMng
{
    public TrailRenderer Trail;
    public Behaviour Halo;
    public HaloMng HaloMng;

    public float condition = 100;
    public float forse = 1;
    private bool activity = true;

    public bool Activity { get => activity; set => activity = value; }

    public float Forse {
        get
        {
            if (activity)
                return forse;
            else
                return 0;
        } 
    }

    public float Condition => condition;

    public void Damage(Damage D)
    {
        condition -= D.Points;
        if (condition < 0)
            condition = 0;
    }

    public void Off()
    {
        activity = false;
        Trail.enabled = false;
        Halo.enabled = false;
    }

    public void On()
    {
        activity = true;
        Trail.enabled = true;
        Halo.enabled = true;
    }

    public void Repear(float points)
    {
        condition += points;
    }

    public void StartBlink()
    {
        HaloMng.SetBlink(true);
    }

    public void StopBlink()
    {
        HaloMng.SetBlink(false);
    }
}

