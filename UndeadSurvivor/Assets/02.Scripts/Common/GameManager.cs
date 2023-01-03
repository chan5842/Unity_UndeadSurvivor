using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 스크립트
    public PoolManager pool;
    public PlayerCtrl playerCtrl;
    Timer timer;

    // 총알 프리펩 오브젝트 풀링
    public List<GameObject> bulletPools = new List<GameObject>();
    GameObject bulletPrefab;
    int maxCount = 30;          // 총알 오브젝트 풀링 최대 개수

    // 시간 관련
    public float gameTime = 0f;         // 게임 시간

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        bulletPrefab = Resources.Load("Bullet 3") as GameObject;

        timer = GetComponent<Timer>();
    }

    private void Start()
    {
        CreateBulletPool();
    }

    #region 총알 오브젝트 풀링
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
    #endregion

    void Update()
    {
        gameTime = timer.curTime;

        if(gameTime > timer.maxTime)
        {
            gameTime = timer.maxTime;
        }

        //if (playerCtrl.gameObject.GetComponent<PlayerDamage>().dead)
        if (PlayerManager.instance.playerDamage.dead)
        {
            Debug.Log("캐릭터 사망");
            return;
        }
            
            
    }
}
