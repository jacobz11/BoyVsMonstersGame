using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    #region Bullet Shoot
    private GameObject bullet1;
    private GameObject bullet2;
    private GameObject bullet3;
    private float bulletSpeed = 10f;
    private float bulletLife = 1f;
    private Vector3 shootLeft = new(-1, 0, 0);
    private Quaternion quaternion = Quaternion.Euler(0, 180, 0);
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnBullet1;
    [SerializeField] private Transform spawnBullet2;
    [SerializeField] private Transform spawnBullet3;
    private bool shoot;
    #endregion

    #region Enemy Health Parameters
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject deathEffectPrefab;
    private Vector3 deathEffectPos;
    private GameObject deathEffect;
    private float currHealth;
    private float fraction;
    private float damage = 1f;
    private float maxHealth = 3f;
    private float animDelay;
    private float animDuration = 0.5f;
    #endregion
    void Start()
    {
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
    }
    void Update()
    {
        if (shoot)
        {
            bullet1 = Instantiate(bulletPrefab, spawnBullet1.position, quaternion);
            bullet2 = Instantiate(bulletPrefab, spawnBullet2.position, quaternion);
            bullet3 = Instantiate(bulletPrefab, spawnBullet3.position, quaternion);
            bullet1.GetComponent<Rigidbody2D>().velocity = shootLeft * bulletSpeed;
            bullet2.GetComponent<Rigidbody2D>().velocity = shootLeft * bulletSpeed;
            bullet3.GetComponent<Rigidbody2D>().velocity = shootLeft * bulletSpeed;
            Destroy(bullet1, bulletLife);
            Destroy(bullet2, bulletLife);
            Destroy(bullet3, bulletLife);
            shoot = false;
        }
        animDelay += Time.deltaTime;
        if (animDelay >= animDuration)
        {
            animDelay = 0f;
            anim.SetBool("isHit", false);
        }
    }
    public void IsShoot()
    {
        shoot = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            UpdateHealth();
            anim.SetBool("isHit", true);
        }        
    }
    private void UpdateHealth()
    {
        currHealth -= damage;
        if (currHealth <= 0)
        {
            deathEffectPos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
            Destroy(gameObject, 0.25f);
            deathEffect = Instantiate(deathEffectPrefab, deathEffectPos, Quaternion.identity);
            Destroy(deathEffect, 0.4f);
        }
        fraction = currHealth / maxHealth;
        healthBar.fillAmount = fraction;
    }
}
