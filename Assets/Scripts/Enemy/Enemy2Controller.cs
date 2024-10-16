using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2Controller : MonoBehaviour
{
    private Animator anim;

    #region Enemy Health Parameters
    [SerializeField] private Image healthBar;
    private float currHealth;
    private float fraction;
    private float damage = 1f;
    private float maxHealth = 9f;
    private float animDelay;
    private float animDuration = 0.5f;
    [SerializeField] private GameObject deathEffectPrefab;
    private GameObject deathEffect;
    private Vector3 deathEffectPos;
    #endregion

    #region Bullet Related Parameters
    private GameObject bullet;
    private float bulletSpeed = 10f;
    private float bulletLife = 1f;
    private Vector3 shootLeft = new(-1, 0, 0);
    private Quaternion quaternion = Quaternion.Euler(0, 180, 0);
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnBullet;
    private bool shoot;
    #endregion
    void Start()
    {
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
    }

    void Update()
    {
        animDelay += Time.deltaTime;
        if (animDelay >= animDuration)
        {
            animDelay = 0f;
            anim.SetBool("isHit", false);
        }
        if (shoot)
        {
            bullet = Instantiate(bulletPrefab, spawnBullet.position, quaternion);
            bullet.GetComponent<Rigidbody2D>().velocity = shootLeft * bulletSpeed;
            Destroy(bullet, bulletLife);
            shoot = false;
        }
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
            deathEffectPos = new Vector3(transform.position.x, transform.position.y - 1.2f, transform.position.z);
            Destroy(gameObject, 0.25f);
            deathEffect = Instantiate(deathEffectPrefab, deathEffectPos, Quaternion.identity);
            Destroy(deathEffect, 0.4f);
        }
        fraction = currHealth / maxHealth;
        healthBar.fillAmount = fraction;
    }
    public void IsShootRifle()
    {
        shoot = true;
    }
}
