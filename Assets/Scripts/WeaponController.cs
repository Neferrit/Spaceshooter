using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float rpm;
    public float delay;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rpm = 60 / fireRate;

        InvokeRepeating("Fire", delay, rpm);
    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }
}
