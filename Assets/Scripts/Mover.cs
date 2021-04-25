using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rib;
    public float speed;
    private void Start()
    {
        rib = GetComponent<Rigidbody>();
        rib.velocity = transform.forward * speed;
    }
}
