using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Boundaries
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float turboMultiplier;
    public float tilt;
    float turbo = 1.0f;
    
    GamepadInput gamepad;
    
    public Rigidbody rib;
    public Boundaries boundary;
    Vector2 move;
    
    void Awake()
    {
        gamepad = new GamepadInput();

        gamepad.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        gamepad.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        gamepad.Gameplay.Turbo.performed += ctx => turbo = turboMultiplier;
        gamepad.Gameplay.Turbo.canceled += ctx => turbo = 1.0f;

        gamepad.Gameplay.Shoot.performed += ctx => Shoot();
        gamepad.Gameplay.Special.performed += ctx => Special();
    }
    private void FixedUpdate()
    {
        Vector3 velocity = new Vector3(move.x, 0.0f, move.y) * Time.deltaTime * speed * turbo;
        rib.velocity = velocity;
        rib.position = new Vector3(Mathf.Clamp(rib.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rib.position.z, boundary.zMin, boundary.zMax));
        rib.rotation = Quaternion.Euler(0.0f, 0.0f, rib.velocity.x * -tilt);
    }
    void Shoot()
    {
        transform.localScale *= 1.1f;
    }
    void Special()
    {
        transform.localScale /= 1.1f;
    }

    private void OnEnable()
    {
        gamepad.Gameplay.Enable();
    }
    private void OnDisable()
    {
        gamepad.Gameplay.Disable();
    }
}
