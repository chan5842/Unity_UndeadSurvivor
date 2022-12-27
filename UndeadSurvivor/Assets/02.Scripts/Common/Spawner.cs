using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;

public class Spawner : MonoBehaviour
{
    float timer;

    public Transform[] spawnPoint;
    public SpawnData[] spawnDatas;

    int level;
    
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (GameManager.instance.playerCtrl.GetComponent<PlayerDamage>().dead)
            return;
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnDatas.Length-1);

        if (timer > spawnDatas[level].spawnTime)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetEnemyPool(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<EnemyAI>().Init(spawnDatas[level]);
    }
}
