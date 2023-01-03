using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OribitObject : MonoBehaviour
{
    [SerializeField]
    Transform target;           // 중심축
    Transform tr;               // 트랜스폼 정보
    public float oribitSpeed;   // 공전 속도
    Vector3 offset;             // 중심축과의 거리

    BoxCollider2D[] boxColliders;

    void Awake()
    {
        tr = transform;
        boxColliders = GetComponentsInChildren<BoxCollider2D>();
    }

    private void Start()
    {
        target = GameManager.instance.playerCtrl.transform;
        offset = tr.position - target.position;
    }
    void Update()
    {
        if (PlayerManager.instance.playerDamage.dead)
        {
            foreach (var col in boxColliders)
                col.enabled = false;
            return;
        }
            
        tr.position = target.position + offset;
        tr.RotateAround(target.position, Vector3.forward, oribitSpeed * Time.deltaTime);
        offset = tr.position - target.position;
    }
}
