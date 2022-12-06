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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
