using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataInfo;

public class PlayerDamage : LivingObject
{
    // UI
    public Image hpImage;       // ü�� UI

    // ����Ʈ
    public AudioClip deathClip; // ��� �Ҹ�
    public AudioClip[] hitClip; // �ǰ� �Ҹ� �迭
    public AudioClip pickUpClip;// ������ ���� �Ҹ�
    public ParticleSystem destroyEffect; // ����� ������ ����Ʈ

    Collider2D col;

    // ������Ʈ
    AudioSource source;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public GameObject Weapon;

    public int character_Level = 1;
    public int EXP = 0;
    public int[] needExp = { 0, 5, 10, 18, 29, 43, 60, 80, 103, 129, 158, 190 };
    public Image EXPImg;
    public Text LevelText;
    public AudioClip LevelUpClip;

    // ��ũ��Ʈ
    PlayerCtrl playerCtrl;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        playerCtrl = GetComponent<PlayerCtrl>();

        EXPImg.fillAmount = EXP / needExp[character_Level];
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
        base.RestoreHp(newHp * maxHP);  // ü���� ȸ���Ҷ��� �ۼ�Ʈ ��ġ�� ȸ��
        hpImage.fillAmount = hp / maxHP;
    }

    public void GainExp(int newExp)
    {
        EXP += newExp;
        // �ʿ� ����ġ�� �ʰ��ߴٸ�
        if(EXP >= needExp[character_Level])
        {
            int overExp = EXP - needExp[character_Level];   // �ʰ��� ����ġ ���
            character_Level++;
            LevelText.text = "Level. " + character_Level;
            EXP = overExp;  // ���� ����ġ�� �ʰ��� ����ġ�� ����
            source.PlayOneShot(LevelUpClip);
        }

        EXPImg.fillAmount = (float)EXP / (float)needExp[character_Level];
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if (!dead)  // ���� �ʾҴٸ� �������� ���� �� 2�� �� �ϳ��� �ǰ� �Ҹ� ���
            source.PlayOneShot(hitClip[Random.Range(0,1)]);
    
        base.OnDamage(damage, hitPoint, hitNormal);
        hpImage.fillAmount = hp / maxHP;
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
        if(Input.GetKeyDown(KeyCode.R))
        {
            hp -= 10;
            hpImage.fillAmount = hp / maxHP;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dead)
            return;
        //if (other.CompareTag("ITEM"))
        //{
        //    Debug.Log("������ ȹ��");
        //    //itemȹ�� �ڵ�
        //    Item item = other.GetComponent<ItemData>().itemInfo;

        //    if (item != null)
        //    {
        //        Use(item);
        //        //source.PlayOneShot(itemPickupClip);
        //    }
        //    //Destroy(other.gameObject);
        //}
    }

    public void Use(Item item)
    {   
        switch (item.type)
        {
            case Item.ItemType.EXP:
                EXP += Mathf.FloorToInt(item.value);
                EXPImg.fillAmount = EXP / needExp[character_Level];
                break;
            case Item.ItemType.HEAL:
                RestoreHp(item.value);
                break;

            case Item.ItemType.MAGNET:
                // �ڼ� ������ ȿ�� �ߵ�(�ֺ� �ݰ� X m���� ��� ����ġ�� ���Ƶ���)
                break;
        }
        
    }
}
