using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    PlayerDamage playerDamage;

    public Canvas UI;
    [SerializeField]
    Image GameOverImg;
    [SerializeField]
    Text killText;

    int killCount = 0;

    void Awake()
    {
        instance = this;
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>();
        GameOverImg = UI.transform.GetChild(4).GetComponent<Image>();
        killText = UI.transform.GetChild(1).GetChild(1).GetComponent<Text>();
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

    public void InKillCount()
    {
        killCount++;
        killText.text = killCount.ToString();
    }
}
