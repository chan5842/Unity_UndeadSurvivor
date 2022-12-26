using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : LivingObject
{
    [SerializeField]
    LivingObject target;                    // ���ʹ̰� ������ �÷��̾� ������Ʈ
    Vector2 moveDir;                        // ���ʹ̰� ������ ĳ������ ����

    public ParticleSystem deadEffect;       // ��� ����Ʈ
    public AudioClip deathSound;            // ����Ҹ�
    public AudioClip hitSound;              // �ǰ� �Ҹ�

    // ������Ʈ
    Animator animator;
    AudioSource source;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public RuntimeAnimatorController[] animCon; // �ִϸ����� ����


    // ������ �޾ƿ���
    public float damage;              // ���ݷ�
    public float attackSpeed = 0.5f;  // ���� ����
    public float lastAttackTime;      // ���������� ������ �ð�
    public float moveSpeed;           // �� �̵��ӵ�


    void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        deadEffect.Stop();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        // �÷��̾ ã��
        target = GameManager.instance.playerCtrl.GetComponent<LivingObject>();
        dead = false;
        hp = maxHP;
    }
    public void Init(SpawnData data)
    {
        // �ִϸ����� ����
        animator.runtimeAnimatorController =  animCon[data.spriteType];
        moveSpeed = data.moveSpeed;
        maxHP = data.hp;
        hp = maxHP;
        damage = data.damage;
    }

    private void OnDisable()
    {
        // ��� �ݶ��̴��� �˻��Ͽ� Ȱ��ȭ(����� ��Ȱ��ȭ �Ǿ��� ������ �ٽ� Ȱ��ȭ)
        Collider2D[] enemyColliders = GetComponents<Collider2D>();
        foreach (var col in enemyColliders)
        {
            col.enabled = true;
        }
        // ������Ʈ Ǯ������ ��Ȱ��ȭ �Ǿ��� ������ ������Ʈ ��ġ�� �ʱ�ȭ
        transform.position = Vector2.zero;
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

    //// ���� Ȱ��ȭ �Ǿ��� �� ���� ��
    //public void Setup(float newHp, float newDamage, float newSpeed, Sprite newSprite)
    //{
    //    base.maxHP = newHp;
    //    hp = base.maxHP;
    //    damage = newDamage;
    //    moveSpeed = newSpeed;
    //    spriteRenderer.sprite = newSprite;
    //}

    private void Start()
    {
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }

    // �÷��̾ �����ϴ� �Լ�
    void MoveToPlayer()
    {
        if (dead)   // �׾��ٸ� ���� X
            return;
        if (hasTarget)  // Ÿ���� �ִٸ�(�÷��̾ ���� �ʾҴٸ�)
        {
            // �÷��̾���� �Ÿ��� 0���� ũ�ٸ�
            if (Vector2.Distance(transform.position, target.transform.position) > 0)
            {
                moveDir = (target.transform.position - transform.position).normalized; // ����
                Vector2 nextVec = moveDir * moveSpeed * Time.fixedDeltaTime;           // �̵� ����
                rb.MovePosition(rb.position + nextVec);                                // �̵�
                rb.velocity = Vector2.zero;     // ���� �ӵ��� �̵��� ������ ���� �ʵ��� �߰�
            }    
        }
        else // Ÿ���� ���ٸ�(�÷��̾ �׾��ٸ�)
        {
            moveDir = Vector2.zero;     // �̵� ������ 0f;
            rb.velocity = Vector2.zero; // �̵� �ӵ� ���� 0f;
        }
    }

    private void LateUpdate()
    {
        if (moveDir.x != 0f)         // ĳ���� ���⿡ ���� flip����
            spriteRenderer.flipX = moveDir.x < 0;
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if(!dead) // ���� �ʾ����� �������� ������
        {
            //// ���ݹ��� ������ �������� ��ƼŬ ȿ�� ���
            //hitEffect.transform.position = hitPoint;
            //hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            //hitEffect.Play();

            // �ǰ��� ���
            source.PlayOneShot(hitSound);
            //animator.SetTrigger("Hit");
            StartCoroutine(Hit());
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    IEnumerator Hit()
    {
        // 0.5�� �������� ���� ������ ���ƿ�
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }

    public override void Die()
    {
        base.Die();

        // �ٸ� AI�� �������� �ʵ��� ��� �ݶ��̴� ��Ȱ��ȭ
        Collider2D[] enemyColliders = GetComponents<Collider2D>();
        foreach(var col in enemyColliders)
        {
            col.enabled = false;
        }

        // �̵��� ���߰� Die�ִϸ��̼� ���� �� ��� �Ҹ� ���
        moveDir = Vector2.zero;
        animator.SetBool("Dead", true);
        source.PlayOneShot(deathSound);
        deadEffect.Play();

        UIManager.instance.InKillCount();

        StartCoroutine(EnemyDie());       
    }

    IEnumerator EnemyDie()
    {
        // 2���� ���ʹ� ������Ʈ ��Ȱ��ȭ
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        // ���� �ʾҰ� ���ݰ��ݸ��� ������
        if (!dead && Time.time >= lastAttackTime + attackSpeed)
        {
            LivingObject attackTarget = col.collider.GetComponent<LivingObject>();

            if (attackTarget != null && attackTarget == target)
            {
                lastAttackTime = Time.time;

                // ������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ���
                Vector2 hitPoint = col.collider.ClosestPoint(transform.position);
                Vector2 hitNormal = transform.position - col.collider.transform.position;

                // ����
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }

        }
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if(!dead && Time.time >= lastAttackTime + attackSpeed)
    //    {
    //        LivingObject attackTarget = other.GetComponent<LivingObject>();

    //        if(attackTarget != null && attackTarget == target)
    //        {
    //            lastAttackTime = Time.time;

    //            // ������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ���
    //            Vector2 hitPoint = other.ClosestPoint(transform.position);
    //            Vector2 hitNormal = transform.position - other.transform.position;

    //            // ����
    //            attackTarget.OnDamage(damage, hitPoint, hitNormal);
    //        }
           
    //    }
    //}
}
