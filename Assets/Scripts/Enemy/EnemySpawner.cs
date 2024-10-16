using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private GameObject wonMenu;
    [SerializeField] private Transform[] enemies1SpawnPointers;
    [SerializeField] private Transform[] enemies2SpawnPointers;
    private GameObject[] enemies;
    private int numOfEnemies;
    private Quaternion quaternion = Quaternion.Euler(0f, 180f, 0f);
    private int i, j, score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private bool isAllDead;
    void Start()
    {
        numOfEnemies = enemies1SpawnPointers.Length + enemies2SpawnPointers.Length;
        enemies = new GameObject[numOfEnemies];
        for (i = 0; i < enemies1SpawnPointers.Length; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, enemies1SpawnPointers[i].position, quaternion);
        }

        j = enemies1SpawnPointers.Length;
        for (i = 0; i < enemies2SpawnPointers.Length; i++)
        {
            enemies[j] = Instantiate(enemyPrefab2, enemies2SpawnPointers[i].position, quaternion);
            j++;
        }
    }
    void Update()
    {
        score = 0;
        isAllDead = true;
        for (i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
                score++;
            else
                isAllDead = false;
        }
        if (isAllDead)
        {
            wonMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        scoreText.text = "Score: " + score + "/" + numOfEnemies;
    }
}
