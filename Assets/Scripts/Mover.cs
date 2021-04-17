using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    Rigidbody rib;
    public float speed;
    private void Start()
    {
        rib.velocity = transform.forward * speed;
    }
}
