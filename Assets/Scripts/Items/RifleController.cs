using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RifleController : MonoBehaviour
{
    private Animator anim;
    private int bulletsCounter = 0;
    private float bulletDelay = 0f;
    private float timeCount = 2.3f;
    [SerializeField] private GameObject enemy2Prefab;

    public UnityEvent readyToShootRifle;
    private void Start()
    {
        anim = GetComponent<Animator>();
        readyToShootRifle.AddListener(enemy2Prefab.GetComponent<Enemy2Controller>().IsShootRifle);
    }
    private void Update()
    {
        bulletDelay += Time.deltaTime;
        if (bulletsCounter < 5)
            anim.SetBool("isShoot", true);

        if (bulletDelay >= timeCount)
        {
            bulletDelay = 0f;
            bulletsCounter = 0;
        }
    }

    // Function of animation event in the shoot animation of the Rifle
    public void Shoot()
    {
        if (bulletsCounter < 5)
        {
            bulletsCounter++;
            readyToShootRifle.Invoke();
        } else anim.SetBool("isShoot", false);
    }
}
