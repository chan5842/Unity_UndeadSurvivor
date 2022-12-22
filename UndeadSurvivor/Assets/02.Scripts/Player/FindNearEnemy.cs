using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearEnemy : MonoBehaviour
{
    FireCtrl fireCtrl;

    [SerializeField]
    Collider2D[] enemys;        // ���ʹ� ���� �ݶ��̴�
    [SerializeField]
    LayerMask enemyLayer;       // ���ʹ� ���̾�
    public GameObject nearEnemy;// ���� �ٰŸ��� �ִ� ���ʹ�
    float shortDist;            // ���� ����� �Ÿ� ���ϴ� ����

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
