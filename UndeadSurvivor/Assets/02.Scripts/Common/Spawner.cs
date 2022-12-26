using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

[System.Serializable]       // 직렬화(인스펙터 창에서 보이지 않는것을 볼 수 있게 함)
public class SpawnData
{
    public int spriteType;  // 몬스터 종류
    public float spawnTime; // 스폰 간격
    public int hp;          // 몬스터의 체력
    public float moveSpeed; // 몬스터의 이동속도
    public int damage;      // 몬스터의 공격력
}
