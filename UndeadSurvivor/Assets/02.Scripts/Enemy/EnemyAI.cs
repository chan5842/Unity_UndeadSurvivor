using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : LivingObject
{
    [SerializeField]
    LivingObject target;                    // ���ʹ̰� ������ �÷��̾� ������Ʈ
    [SerializeField]
    Transform targetTr;                     // �÷��̾� ������Ʈ Ʈ������

    public ParticleSystem hitEffect;        // �ǰ� ����Ʈ
    public AudioClip deathSound;            // ����Ҹ�
    public AudioClip hitSound;              // �ǰ� �Ҹ�

    Animator animator;
    AudioSource source;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    // ��ũ���ͺ� ������Ʈ�� ������ �޾ƿ���
    public float damage = 20f;              // ���ݷ�
    public float attackSpeed = 0.5f;        // ���� ����
    float lastAttackTime;                   // ���������� ������ �ð�
    float moveSpeed;                        // �� �̵��ӵ�

    void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        // �÷��̾ ã��
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingObject>();
        targetTr = target.GetComponent<Transform>();
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

    // ���� Ȱ��ȭ �Ǿ��� �� ���� ��
    public void Setup(float newHp, float newDamage, float newSpeed, Sprite newSprite)
    {
        initHp = newHp;
        hp = initHp;
        damage = newDamage;
        moveSpeed = newSpeed;
        spriteRenderer.sprite = newSprite;
    }

    private void Start()
    {
        //StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        if(rb.velocity.x != 0f)        
            spriteRenderer.flipX = rb.velocity.x < 0;

        //MoveToPlayer();
    }
    void FixedUpdate()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (dead)
            return;
        if (hasTarget)
        {
            if (Vector2.Distance(transform.position, targetTr.position) > 0)
                transform.position =
                    Vector2.MoveTowards(transform.position, targetTr.position, moveSpeed * Time.fixedDeltaTime);
        }
        //else
        //    rb.velocity = Vector2.zero;
    }
}
