using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;     // �������� ������ ����
    List<GameObject>[] Pools;        // ������Ʈ Ǯ�� ��� ����Ʈ

    private void Awake()
    {
        Pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i< Pools.Length; i++)
        {
            Pools[i] = new List<GameObject>();
        }
    }

    // Ǯ���� ���ʹ� ���ÿ��� ��������
    public GameObject GetEnemyPool(int i)
    {
        GameObject select = null;

        foreach(GameObject obj in Pools[i])
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
            select = Instantiate(prefabs[i], transform);
            Pools[i].Add(select);
        }

        return select;
    }    

}
