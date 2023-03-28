using UnityEngine;
using SampleSpace;

public class TrusterMng : SystemMng, ITrusterMng
{
    public TrailRenderer Trail;
    public Behaviour Halo;
    public HaloMng HaloMng;

    public float forse = 1;

    public float Forse {
        get
        {
            if (status == Statuses.On)
                return forse;
            else
                return 0;
        } 
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

