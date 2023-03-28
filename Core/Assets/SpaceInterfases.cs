using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SampleSpace
{
    public interface ISystemMng
    {
        void Off();
        void On();
        Statuses Status { get; }
        float Condition { get; }
        void Damage(Damage d);
        void Repear(float poin);
    }

    public interface ITrusterMng : ISystemMng
    {
        float Forse { get; }
        void StartBlink();
        void StopBlink();
    }

    public interface IWeaponMng : ISystemMng
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
    public enum Statuses
    {
        Off = 0,
        On = 1,
        Alert = 3
    }

    public interface ICraftMng : ISystemMng
    {
        void OffTrusters();
        void OnTrusters();
        bool TrustersActivity { get; set; }
        void Move(InputAction.CallbackContext context);
        void Rotare(InputAction.CallbackContext context);
        void Brake(InputAction.CallbackContext context);
        void CaptureMovement(Transform target);
        void Fire(InputAction.CallbackContext context);
        Transform GetCameraRoot();
        float GetSpeed();
        Statuses TrastersStatus { get; }
    }

    public interface ICameraMng
    {
        public void SetCraft(ICraftMng craftMng);
    }
}

