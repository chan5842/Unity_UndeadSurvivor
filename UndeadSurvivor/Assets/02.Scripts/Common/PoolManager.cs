using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;     // 프리펩을 보관할 변수
    List<GameObject>[] Pools;        // 오브젝트 풀링 담당 리스트

    private void Awake()
    {
        Pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i< Pools.Length; i++)
        {
            Pools[i] = new List<GameObject>();
        }
    }

    // 풀에서 에너미 선택에서 꺼내오기
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
