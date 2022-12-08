using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    LivingObject target;                    // ���ʹ̰� ������ �÷��̾� ������Ʈ

    public ParticleSystem hitEffect;        // �ǰ� ����Ʈ
    public AudioClip deathSound;            // ����Ҹ�
    public AudioClip hitSound;              // �ǰ� �Ҹ�

    Animator animator;
    AudioSource source;
    SpriteRenderer spriteRenderer;

    // ��ũ���ͺ� ������Ʈ�� ������ �޾ƿ���
    public float damage = 20f;              // ���ݷ�
    public float attackSpeed = 0.5f;        // ���� ����
    float lastAttackTime;                   // ���������� ������ �ð�

    private void OnEnable()
    {
        // �÷��̾ ã��
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingObject>();
    }
    bool hasTarget  // Ÿ���� �����ϴ��� Ȯ���� ������Ƽ
    {
        get
        {
            if (target != null && !target.dead)
            {
                return true;
            }
            return false;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
