using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    // Configuration Parameters
    [Header("Player")] [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private int health = 200;

    [SerializeField] private float spaceShipPadding = 1f;

    [Header("Projectile")] [FormerlySerializedAs("lazerAmmo")] [SerializeField] [CanBeNull]
    private GameObject laserAmmo;

    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileFiringPeriod = 0.02f;
    [SerializeField] private AudioClip playerDieSound;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] private float playerDieSoundVolume = 0.7f;

    private Coroutine firingCoroutine;

    private float xMinimum;
    private float xMaximum;
    private float yMinimum;
    private float yMaximum;

    private void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        var level = FindObjectOfType<Level>();
        level.LoadGameOver(3);
        AudioSource.PlayClipAtPoint(playerDieSound, Camera.main.transform.position, playerDieSoundVolume);
        Destroy(gameObject);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject lazer = Instantiate(laserAmmo, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, 0.5f);
            lazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXposition = Mathf.Clamp(transform.position.x + deltaX, xMinimum, xMaximum);
        var newYposition = Mathf.Clamp(transform.position.y + deltaY, yMinimum, yMaximum);

        transform.position = new Vector2(newXposition, newYposition);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        if (gameCamera != null) xMinimum = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spaceShipPadding;
        if (gameCamera != null) xMaximum = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spaceShipPadding;
        if (gameCamera != null) yMinimum = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + spaceShipPadding;
        if (gameCamera != null) yMaximum = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - spaceShipPadding;
    }
}