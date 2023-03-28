using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGrouapMng : MonoBehaviour
{
    public Rigidbody Body;
    public float Torque = 0.5f;
    
    void Start()
    {
        Body.AddRelativeTorque(Vector3.up * Torque, ForceMode.VelocityChange);    
    }

    void FixedUpdate()
    {
       //transform.localEulerAngles = new Vector3(0, transform.rotation.y, 0);
    }
}
