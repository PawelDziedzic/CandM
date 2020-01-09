using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompilationScript : MonoBehaviour
{
    Rigidbody myRb;

    void OnEnable()
    {
        myRb = GetComponent<Rigidbody>();

        
    }

    void FixedUpdate()
    {
        myRb.AddForce(Vector3.left * 0.8f, ForceMode.VelocityChange);
    }
}
