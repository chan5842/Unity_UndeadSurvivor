using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;

public class ItemData : MonoBehaviour
{
    public Item itemInfo;

    [SerializeField]
    Collider2D[] cols;

    [SerializeField]
    LayerMask expLayer;

    private void Start()
    {
        expLayer = LayerMask.NameToLayer("EXP");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch(itemInfo.type)
            {
                case Item.ItemType.EXP:
                    other.GetComponent<PlayerDamage>().GainExp((int)(itemInfo.value));
                    break;
                case Item.ItemType.HEAL:
                    other.GetComponent<PlayerDamage>().RestoreHp(itemInfo.value);
                    break;
                case Item.ItemType.MAGNET:
                    //int layerMask = 1 << expLayer;
                    //cols = Physics2D.OverlapCircleAll(other.transform.position, itemInfo.value, layerMask);
                    //foreach (var col in cols)
                    //{
                    //    col.gameObject.GetComponent<ItemExp>().isCollect = true;
                    //}
                    PlayerManager.instance.playerCtrl.MagnetExp();
                    break;

            }
            gameObject.SetActive(false);

        }
    }

}
