using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerCtrl playerCtrl;

    // 총알 프리펩 오브젝트 풀링
    public List<GameObject> bulletPools = new List<GameObject>();
    GameObject bulletPrefab;
    int maxCount = 30;          // 총알 오브젝트 풀링 최대 개수

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        bulletPrefab = Resources.Load("Bullet 3") as GameObject;
    }

    private void Start()
    {
        CreateBulletPool();
    }

    void CreateBulletPool()
    {
        GameObject bulletPool = new GameObject("BulletPool");
        for (int i = 0; i < maxCount; i++)
        {
            var obj = Instantiate<GameObject>(bulletPrefab, bulletPool.transform);
            obj.name = "Bullet_" + i.ToString("00");
            obj.SetActive(false);
            bulletPools.Add(obj);
        }
    }
    public GameObject GetBullet()
    {
        for (int i = 0; i < bulletPools.Count; i++)
        {
            if (bulletPools[i].activeSelf == false)
            {
                return bulletPools[i];
            }
        }
        return null;
    }


    void Update()
    {
        
    }
}
