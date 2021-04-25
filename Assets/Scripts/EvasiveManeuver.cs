using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public Boundaries boundary;

    public float dodge;
    public float tilt;
    public float smoothing;
    public Vector2 startWait;
    public Vector2 maneuverWait;
    public Vector2 maneuverTime;

    private float newManeuver;
    private float targetManeuver;
    private float currSpeed;
    private Rigidbody rib;
    void Start()
    {
        rib = GetComponent<Rigidbody>();
        currSpeed = rib.velocity.z;
        StartCoroutine("Evade");
    }
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
    void FixedUpdate()
    {
        newManeuver = Mathf.MoveTowards(rib.velocity.x, targetManeuver, Time.fixedDeltaTime * smoothing);
        rib.velocity = new Vector3(newManeuver, 0.0f, currSpeed);
        rib.position = new Vector3(Mathf.Clamp(rib.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rib.position.z, boundary.zMin, boundary.zMax));
        rib.rotation = Quaternion.Euler(0.0f, 0.0f, rib.velocity.x * -tilt);
    }
}
