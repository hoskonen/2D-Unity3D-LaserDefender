using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private float shotCounter;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 0.5f;
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private float durationOfExplosion = 1f;
    [SerializeField] private AudioClip enemyDieSound;
    [SerializeField] private AudioClip enemyLaser;
    [SerializeField] [Range(0, 1)] private float enemyLaserVolume = 0.5f;

    private void CountdownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject ammo = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
        ammo.GetComponent<Rigidbody2D>().velocity += new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(enemyLaser, Camera.main.transform.position, enemyLaserVolume);
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
        AudioSource.PlayClipAtPoint(enemyDieSound, transform.position);
        GameObject explosion =
            Instantiate(explosionParticle, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(explosion, durationOfExplosion);
    }

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountdownAndShoot();
    }
}