using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : LivingObject
{
    public Image hpImage;       // ü�� UI

    public AudioClip deathClip; // ��� �Ҹ�
    public AudioClip[] hitClip; // �ǰ� �Ҹ� �迭
    public AudioClip pickUpClip;// ������ ���� �Ҹ�

    public ParticleSystem destroyEffect; // ����� ������ ����Ʈ

    Collider2D col;

    AudioSource source;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public GameObject Weapon;

    PlayerCtrl playerCtrl;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        playerCtrl = GetComponent<PlayerCtrl>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        hpImage.gameObject.SetActive(true);
        hpImage.fillAmount = 1f;

        destroyEffect.Stop();
        playerCtrl.enabled = true;
    }

    public override void RestoreHp(float newHp)
    {
        base.RestoreHp(newHp);
        hpImage.fillAmount = hp / initHp;
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if (!dead)  // ���� �ʾҴٸ� �������� ���� �� 2�� �� �ϳ��� �ǰ� �Ҹ� ���
            source.PlayOneShot(hitClip[Random.Range(0,1)]);
    
        base.OnDamage(damage, hitPoint, hitNormal);
        hpImage.fillAmount = hp / initHp;
        StartCoroutine(OnHit());
        
    }

    IEnumerator OnHit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = Color.white;
    }

    public override void Die()
    {
        base.Die();

        col.enabled = false;
        hpImage.gameObject.SetActive(false);
        Weapon.SetActive(false);
        source.PlayOneShot(deathClip);
        animator.SetTrigger("Dead");
        destroyEffect.Play();

        playerCtrl.enabled = false;
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {   
            // itemȹ�� �ڵ�
            //IItem item = other.GetComponent<IItem>();

            //if (item != null)
            //{
            //    item.Use(gameObject);
            //    source.PlayOneShot(itemPickupClip);
            //}
        }
    }
}
