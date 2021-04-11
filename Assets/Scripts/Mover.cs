using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    Rigidbody rib;
    public float speed;
    public GameObject shot;
    private void Start()
    {
        rib.velocity = transform.forward * speed;
        //Destroy(shot, 1f); when no boundary
    }
}
