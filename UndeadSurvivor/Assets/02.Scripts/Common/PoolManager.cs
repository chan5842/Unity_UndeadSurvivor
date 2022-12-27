using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;   // 프리펩을 보관할 변수
    List<GameObject>[] enemyPools;      // 오브젝트 풀링 담당 리스트

    public GameObject[] expPrefabs;     // 경험치 프리펩(0, 1, 2)
    List<GameObject>[] expPools;        // 경험치 풀

    public GameObject[] itemPrefabs;    // 아이템 프리펩(회복, 자석)
    List<GameObject>[] itemPools;       // 아이템 풀
    private void Awake()
    {
        enemyPools = new List<GameObject>[enemyPrefabs.Length];
        for(int i = 0; i< enemyPools.Length; i++)
            enemyPools[i] = new List<GameObject>();

        expPools = new List<GameObject>[expPrefabs.Length];
        for(int i=0; i<expPools.Length; i++)
            expPools[i] = new List<GameObject>();

        itemPools = new List<GameObject>[itemPrefabs.Length];
        for(int i=0; i<itemPools.Length; i++)
            itemPools[i] = new List<GameObject>();
    }

    // 풀에서 에너미 선택에서 꺼내오기
    public GameObject GetEnemyPool(int i)
    {
        GameObject select = null;

        foreach(GameObject obj in enemyPools[i])
        {
            if(!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        if(!select)
        {
            select = Instantiate(enemyPrefabs[i], transform);
            enemyPools[i].Add(select);
        }

        return select;
    }    

    // 풀에서 경험치 선택해서 꺼내오기
    public GameObject GetExpPool(int i)
    {
        GameObject select = null;

        foreach(GameObject obj in expPools[i])
        {
            if(!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        if(!select)
        {
            select = Instantiate(expPrefabs[i], transform);
            expPools[i].Add(select);
        }

        return select;
    }

    public GameObject GetItemPool(int i)
    {
        GameObject select = null;
        
        foreach(GameObject obj in itemPools[i])
        {
            if(!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        if(!select)
        {
            select = Instantiate(itemPrefabs[i], transform);
            expPools[i].Add(select);
        }

        return select;
    }

}
