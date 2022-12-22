using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearEnemy : MonoBehaviour
{
    FireCtrl fireCtrl;

    [SerializeField]
    Collider2D[] enemys;        // 에너미 검출 콜라이더
    [SerializeField]
    LayerMask enemyLayer;       // 에너미 레이어
    public GameObject nearEnemy;// 가장 근거리에 있는 에너미
    float shortDist;            // 가장 가까운 거리 구하는 변수

    float autoTargetRange = 20f;

    void Awake()
    {
        fireCtrl = GetComponent<FireCtrl>();
        enemyLayer = LayerMask.NameToLayer("ENEMY");
    }

    private void OnEnable()
    {
        StartCoroutine(FindNearObject());
    }

    IEnumerator FindNearObject()
    {
        int layerMask = (1 << enemyLayer);
        while (!GetComponent<PlayerDamage>().dead)
        {
            yield return new WaitForSeconds(0.3f);
            enemys = Physics2D.OverlapBoxAll(transform.position, new Vector2(20, 20), 0, layerMask);

            if (enemys.Length != 0)
            {
                shortDist = Vector2.Distance(transform.position, enemys[0].transform.position);
                nearEnemy = enemys[0].gameObject;
                fireCtrl.target = nearEnemy.transform;

                foreach (var enemy in enemys)
                {
                    if (enemy.gameObject == nearEnemy)
                        continue;
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);

                    if (distance < shortDist)
                    {
                        nearEnemy = enemy.gameObject;
                        shortDist = distance;
                        fireCtrl.target = nearEnemy.transform;
                    }
                }
            }
            else
            {
                nearEnemy = null;
                fireCtrl.target = null;
            }
        }        
    }

}
