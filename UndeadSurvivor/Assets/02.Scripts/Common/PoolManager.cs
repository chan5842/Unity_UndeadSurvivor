using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;   // �������� ������ ����
    List<GameObject>[] enemyPools;      // ������Ʈ Ǯ�� ��� ����Ʈ

    public GameObject[] expPrefabs;     // ����ġ ������(0, 1, 2)
    List<GameObject>[] expPools;        // ����ġ Ǯ

    public GameObject[] itemPrefabs;    // ������ ������(ȸ��, �ڼ�)
    List<GameObject>[] itemPools;       // ������ Ǯ
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

    // Ǯ���� ���ʹ� ���ÿ��� ��������
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

    // Ǯ���� ����ġ �����ؼ� ��������
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
