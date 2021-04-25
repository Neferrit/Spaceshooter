using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class Boundaries
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{
    GamepadInput gamepad;

    public Button specialButton;
    public GameObject shot;
    public Transform shotSpawn;
    public Rigidbody rib;
    public GameController gameController;

    public float speed;
    public float turboMultiplier;
    public float tilt;
    float turbo = 1.0f;

    private bool shooting = false;
    private float nextshot = 0.0f;
    public float shotsPerMinute; //shots per minute
    private float shotFrequency; //seconds per shot - calculated from shotsPerMinute
    public float specialCooldown; //special cooldown after specialDisable
    private float nextspecial = 0.0f;
    public float specialDuration; //how long does the special go for
    private float specialDisable = 0.0f;
    public float specSizeMod;
    private Vector3 specialSize;

    public Boundaries boundary;
    Vector2 move;

    public AudioSource weaponSound;

    private void Start()
    {
        shotFrequency = 60 / shotsPerMinute;
        specialSize = new Vector3(specSizeMod, specSizeMod, specSizeMod);
        shot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        specialButton.onClick.AddListener(Special);
    }
    void Awake()
    {
        gamepad = new GamepadInput();

        gamepad.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        gamepad.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        gamepad.Gameplay.Turbo.performed += ctx => turbo = turboMultiplier;
        gamepad.Gameplay.Turbo.canceled += ctx => turbo = 1.0f;

        gamepad.Gameplay.Shoot.performed += ctx => shooting = true;
        gamepad.Gameplay.Shoot.canceled += ctx => shooting = false;

        gamepad.Gameplay.Special.performed += ctx => Special();
    }
    private void FixedUpdate()
    {
        Vector3 velocity = new Vector3(move.x, 0.0f, move.y) * Time.fixedDeltaTime * speed * turbo;
        rib.velocity = velocity;
        rib.position = new Vector3(Mathf.Clamp(rib.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rib.position.z, boundary.zMin, boundary.zMax));
        rib.rotation = Quaternion.Euler(0.0f, 0.0f, rib.velocity.x * -tilt);
    }
    void Update()
    {
        if (shooting && Time.time > nextshot)
        {
            nextshot = Time.time + shotFrequency;
            Instantiate(shot, shotSpawn.position, Quaternion.identity);
            weaponSound.Play();
        }
    }
    IEnumerator SpecialNormalize()
    {
        yield return new WaitForSeconds(specialDisable - Time.time);
        shot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
    public void Special()
    {
        if (Time.time > nextspecial)
        {
            specialDisable = Time.time + specialDuration;
            nextspecial = specialDisable + specialCooldown;
            shot.transform.localScale = specialSize;

            StartCoroutine("SpecialNormalize");
        }
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
