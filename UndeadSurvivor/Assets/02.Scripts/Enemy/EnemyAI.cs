using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : LivingObject
{
    [SerializeField]
    LivingObject target;                    // ���ʹ̰� ������ �÷��̾� ������Ʈ

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

    //IEnumerator UpdatePath()
    //{
    //    while(!dead)
    //    {
    //        if(hasTarget)
    //        {
    //            float distance = Vector2.Distance(transform.position, target.transform.position);
    //            rb.MoveRotation(distance);

    //        }
    //    }
    //}
    private void Update()
    {
        if(rb.velocity.x != 0f)        
            spriteRenderer.flipX = rb.velocity.x < 0;
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
            Vector2 newVec = transform.forward * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + newVec);
        }
    }
}
