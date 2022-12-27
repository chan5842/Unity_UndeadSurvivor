using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;

public class ItemData : MonoBehaviour
{
    public Item itemInfo;

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
                    Collider2D col = Physics2D.OverlapCircle(other.transform.position, 20f);
                    if(col.gameObject.GetComponent<ItemData>().itemInfo.type == Item.ItemType.EXP)
                    {
                        // 플레이어에게 자석처럼 끌려오는 코드
                        Debug.Log("hi");
                        
                    }
                    break;

            }
            gameObject.SetActive(false);

        }
    }

}
