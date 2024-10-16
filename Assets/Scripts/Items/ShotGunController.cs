using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShotGunController : MonoBehaviour
{
    public UnityEvent readyToShoot;
    [SerializeField] private GameObject enemyPrefab;

    private void Start()
    {
        readyToShoot.AddListener(enemyPrefab.GetComponent<EnemyController>().IsShoot);
    }
    // Function of animation event in the shoot animation of the shotgun
    public void Shoot()
    {
        readyToShoot.Invoke();
    }
}
