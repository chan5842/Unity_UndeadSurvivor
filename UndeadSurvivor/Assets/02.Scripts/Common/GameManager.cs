using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // ��ũ��Ʈ
    public PoolManager pool;
    public PlayerCtrl playerCtrl;
    Timer timer;

    // �Ѿ� ������ ������Ʈ Ǯ��
    public List<GameObject> bulletPools = new List<GameObject>();
    GameObject bulletPrefab;
    int maxCount = 30;          // �Ѿ� ������Ʈ Ǯ�� �ִ� ����

    // �ð� ����
    public float gameTime = 0f;         // ���� �ð�

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

    #region �Ѿ� ������Ʈ Ǯ��
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
            Debug.Log("ĳ���� ���");
            return;
        }
            
            
    }
}
