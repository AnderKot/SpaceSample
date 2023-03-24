using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SampleSpace
{
    interface ISystemMng
    {
        void Off();
        void On();
        bool Activity { get; set; }

        float Condition { get; }
        void Damage(Damage d);
        void Repear(float poin);
    }

    interface ITrusterMng : ISystemMng
    {
        float Forse { get; }
        void StartBlink();
        void StopBlink();
    }

    interface IWeaponMng : ISystemMng
    {
        void Fire();
        void HoldFire();
    }

    public record Damage
    {
        public int Points { get; set; }
        public DamageType Type { get; set; }

        public Damage(int points, DamageType type)
        {
            Points = points;
            Type = type;
        }
    }

    public enum DamageType
    {
        Clear = 0,
        Physical = 1,
        Energy = 2,
        Termal = 3,
        Presure = 4,
        EMP = 5
    }

    interface ICraftMng : ISystemMng
    {

        void OffTrusters();
        void OnTrusters();
        bool TrustersActivity { get; set; }
        void AddSpeed(InputAction.CallbackContext context);
        void Rotare(InputAction.CallbackContext context);
        void Brake(InputAction.CallbackContext context);
        void CaptureMovement(Transform target);

        void CaptureMainCamera();

        void Fire(InputAction.CallbackContext context);
    }
}

