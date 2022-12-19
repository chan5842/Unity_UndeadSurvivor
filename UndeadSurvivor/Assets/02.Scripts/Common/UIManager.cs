using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    PlayerDamage playerDamage;

    public Image GameOverImg;

    void Awake()
    {
        instance = this;
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>();
    }

    private void Start()
    {
        //GameOverImg.enabled = false;
    }

    void Update()
    {
        if(playerDamage.dead)
        {
            //GameOverImg.enabled = true;
            GameOverImg.GetComponent<Animator>().SetTrigger("Dead");
        }
    }
}
