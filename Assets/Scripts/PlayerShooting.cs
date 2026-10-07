using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Disparo")]
    public float range = 100f; // Max shooting distance
    public float fireCooldown = 0.2f; // Seconds between shots
    public ParticleSystem muzzleFlash; // Particle system for muzzle flash effect

    [Header("Rayo")]
    public LineRenderer laser; // LineRenderer placed on the MuzzlePoint
    public Transform muzzlePoint; // Tip of the gun barrel
    public float laserDuration = 0.05f; // How long the red ray stays visible

    [Header("Recoil")]
    public Transform gun; // The gun model (child of the camera)
    public float recoilKick = 0.05f; // How far the gun moves back
    public float recoilAngle = 4f; // How many degrees the gun tilts up
    public float recoilReturnSpeed = 15f; // How fast the gun returns to its place

    private float nextFireTime = 0f;
    private Vector3 gunStartPos;
    private Quaternion gunStartRot;
    private Coroutine laserRoutine;

    void Start()
    {
        // Save the gun's original position and rotation (relative to the camera)
        if (gun != null)
        {
            gunStartPos = gun.localPosition;
            gunStartRot = gun.localRotation;
        }

        // Prepare the laser: 2 points (start and end), hidden at the beginning
        if (laser != null)
        {
            laser.positionCount = 2;
            laser.enabled = false;
        }
    }

    void Update()
    {
        // Shoot if the mouse button is pressed and the cooldown has passed
        if (Mouse.current != null && Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireCooldown;
            Shoot();
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
            ApplyRecoil();
        }

        // Smoothly bring the gun back to its original position every frame
        if (gun != null)
        {
            gun.localPosition = Vector3.Lerp(gun.localPosition, gunStartPos, Time.deltaTime * recoilReturnSpeed);
            gun.localRotation = Quaternion.Slerp(gun.localRotation, gunStartRot, Time.deltaTime * recoilReturnSpeed);
        }
    }

    void Shoot()
    {
        // Create a ray from the center of the screen (0.5, 0.5) through the main camera
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // If nothing is hit, the ray goes to the max range
        Vector3 endPoint = ray.origin + ray.direction * range;

        if (Physics.Raycast(ray, out hit, range))
        {
            endPoint = hit.point;

            EnemyHealth Enemy = hit.transform.GetComponent<EnemyHealth>();
            if (Enemy != null)
            {
                Enemy.TakeDamage(25);
            }
        }

        // Show the red ray from the gun barrel to the hit point
        if (laser != null && muzzlePoint != null)
        {
            if (laserRoutine != null) StopCoroutine(laserRoutine);
            laserRoutine = StartCoroutine(ShowLaser(muzzlePoint.position, endPoint));
        }
    }

    IEnumerator ShowLaser(Vector3 start, Vector3 end)
    {
        laser.SetPosition(0, start);
        laser.SetPosition(1, end);
        laser.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laser.enabled = false;
    }

    void ApplyRecoil()
    {
        if (gun == null) return;

        // Push the gun back and tilt it up (negative X rotation = up)
        gun.localPosition -= new Vector3(0f, 0f, recoilKick);
        gun.localRotation = Quaternion.Euler(-recoilAngle, 0f, 0f) * gun.localRotation;
    }
}