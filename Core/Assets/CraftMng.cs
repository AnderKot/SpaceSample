using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleSpace;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class CraftMng : SystemMng, ICraftMng
{
    public Rigidbody Body;
    public Transform CameraRoot;
    [Header("Limits")]
    public float MaxForwardSpeed;
    public float MaxBackwardSpeed;
    public float MaxSideSpeed;
    public float MaxRotareSpeed;
    [Header("Forse Scale")]
    public float ForwardSpeedScale = 1;
    public float BackwardSpeedScale = 1;
    public float SideSpeedScale = 1;
    public float RotareSpeedScale = 1;
    [Header("Drag Settings")]
    public float PasiveDrag = 0.02f;
    public float BrakeDragScale = 1;

    public float PasiveAngularDrag = 0.02f;
    public float BrakeAngularDragScale = 1;

    private Vector3 CurrForse;
    private Vector3 CurrTorque;
    private float CurrDrag;
    private float CurrAngularDrag ;
    private List<ISystemMng> Systems;
    private List<ITrusterMng> Trusters;
    private List<IWeaponMng> Weapons;

    private bool activity = true;

    public bool TrustersActivity { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Move(InputAction.CallbackContext context)
    {
        if (activity & context.performed)
        {
            float forse = 0;
            Trusters.ForEach(traster => forse += traster.Forse);
            Trusters.ForEach(traster => traster.StartBlink());
            Vector2 direction = context.ReadValue<Vector2>();
            if (Vector2.up == direction)
            {
                CurrForse = Vector3.forward * forse * ForwardSpeedScale;
            }
            if ((Vector2.up + Vector2.right) == direction)
            {
                CurrForse = (Vector3.right * forse * SideSpeedScale) + (Vector3.forward * forse * ForwardSpeedScale);
            }
            if ((Vector2.up + Vector2.left) == direction)
            {
                CurrForse = -(Vector3.right * forse * SideSpeedScale) + (Vector3.forward * forse * ForwardSpeedScale);
            }

            if (Vector2.down == direction)
            {
                CurrForse = -(Vector3.forward * forse * BackwardSpeedScale);
            }
            if ((Vector2.down + Vector2.right) == direction)
            {
                CurrForse = (Vector3.right * forse * SideSpeedScale) - (Vector3.forward * forse * BackwardSpeedScale);
            }
            if ((Vector2.down + Vector2.left) == direction)
            {
                CurrForse = -(Vector3.right * forse * SideSpeedScale) - (Vector3.forward * forse * BackwardSpeedScale);
            }

            if (Vector2.right == direction)
            {
                CurrForse = Vector3.right * forse * SideSpeedScale;
            }
            if (Vector2.left == direction)
            {
                CurrForse = -Vector3.right * forse * SideSpeedScale;
            }
        }

        if(context.canceled)
        {
            Trusters.ForEach(traster => traster.StopBlink());
            CurrForse = Vector3.zero;
        }
    }

    public void Rotare(InputAction.CallbackContext context)
    {
        float forse = 0;
        Trusters.ForEach(traster => forse += traster.Forse);

        if (activity & context.started)
        {
            float direction = context.ReadValue<float>();

            CurrTorque.y = direction * forse * RotareSpeedScale;
        }

        if (context.canceled)
        {
            CurrTorque = Vector3.zero;
        }
    }

    private void ClampTorque()
    {
        Vector3 newVelocity = Vector3.zero;

        newVelocity.y = Mathf.Clamp(Body.angularVelocity.y, - MaxRotareSpeed, MaxRotareSpeed);

        Body.angularVelocity = newVelocity;
    }

    private void ClampVelosity()
    {
        Vector3 newVelocity = Vector3.zero;

        Vector3 localVelocity = transform.InverseTransformDirection(Body.velocity);

        localVelocity.z = Mathf.Clamp(localVelocity.z, -MaxBackwardSpeed, MaxForwardSpeed);
        localVelocity.x = Mathf.Clamp(localVelocity.x, -MaxSideSpeed, MaxSideSpeed);

        newVelocity = transform.TransformDirection(localVelocity);

        Body.velocity = newVelocity;
    }

    public void Brake(InputAction.CallbackContext context)
    {
        float forse = 0;
        Trusters.ForEach(traster => forse += traster.Forse);

        if (activity & context.started)
        {
            CurrDrag = forse * BrakeDragScale;
            CurrAngularDrag = forse * BrakeAngularDragScale;
        }

        if (context.canceled)
        {
            CurrDrag = PasiveDrag;
            CurrAngularDrag = PasiveAngularDrag;
        }
    }

    public void CaptureMovement(Transform target)
    {
        throw new System.NotImplementedException();
    }

    new public void Damage(Damage d)
    {
        base.Damage(d);
        if (status == Statuses.Broke)
        { 
            HoldFire();
            OffTrusters();
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (activity & context.started)
        {
            StartFire();
        }

        if (context.canceled)
        {
            HoldFire();
        }
    }

    public void StartFire()
    {
        if(activity)
            Weapons.ForEach(weapon => weapon.Fire());
    }

    public void HoldFire()
    {
        Weapons.ForEach(weapon => weapon.HoldFire());
    }

    new public void Off()
    {
        base.Off();
        Systems.ForEach(system => system.Off());
    }



    new public void On()
    {
        base.On();
        Systems.ForEach(system => system.On());
    }

    public Transform GetCameraRoot()
    {
        if (CameraRoot == null)
            throw new System.NotImplementedException("У корабля не указано посадочное место для камеры");
        return CameraRoot;
    }

    // -- Двигатели --
    public float GetSpeed()
    {
        return Body.velocity.magnitude;
    }

    public Statuses TrastersStatus
    {
        get
        {
            bool IsOff = false;

            foreach(ITrusterMng truster in Trusters)
            {
                if (truster.Status == Statuses.Broke)
                    return Statuses.Broke;
                if (truster.Status == Statuses.Off)
                    IsOff = true;
            }

            if (IsOff)
                return Statuses.Off;
            
            return Statuses.On;
        }
    }
    public void OnTrusters()
    {
        if (activity)
            Trusters.ForEach(truster => truster.On());
    }
    public void OffTrusters()
    {
        Trusters.ForEach(truster => truster.Off());
    }

    void Start()
    {
        Systems  = transform.GetComponentsInChildren<ISystemMng>().ToList();
        Trusters = transform.GetComponentsInChildren<ITrusterMng>().ToList();
        Weapons  = transform.GetComponentsInChildren<IWeaponMng>().ToList();

        Body.drag = PasiveDrag;
        Body.angularDrag = PasiveAngularDrag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Body.AddRelativeForce(CurrForse, ForceMode.Force);
        Body.AddRelativeTorque(CurrTorque, ForceMode.Force);
        Body.drag = CurrDrag;
        Body.angularDrag = CurrAngularDrag;
        ClampVelosity();
        ClampTorque();
    }
}
