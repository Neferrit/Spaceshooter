using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public Rigidbody rib;
    public float tumble;
    
    private void Start()
    {
        rib.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
