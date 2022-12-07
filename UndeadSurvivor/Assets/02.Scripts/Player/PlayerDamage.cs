using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : LivingObject
{
    public Image hpImage;       // ü�� UI

    public AudioClip deathClip; // ��� �Ҹ�
    public AudioClip hitClip;   // �ǰ� �Ҹ�
    public AudioClip pickUpClip;// ������ ���� �Ҹ�

    AudioSource source;
    Animator animator;

    PlayerCtrl playerCtrl;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerCtrl = GetComponent<PlayerCtrl>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        hpImage.gameObject.SetActive(true);
        hpImage.fillAmount = 1f;

        playerCtrl.enabled = true;
    }

    public override void RestoreHp(float newHp)
    {
        base.RestoreHp(newHp);
        hpImage.fillAmount = hp / initHp;
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if (!dead)
            source.PlayOneShot(hitClip);
        base.OnDamage(damage, hitPoint, hitNormal);
        hpImage.fillAmount = hp / initHp;
    }

    public override void Die()
    {
        base.Die();

        hpImage.gameObject.SetActive(false);
        source.PlayOneShot(deathClip);
        animator.SetTrigger("Die");

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
