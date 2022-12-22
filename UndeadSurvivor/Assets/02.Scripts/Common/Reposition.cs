using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    [SerializeField]
    Collider2D enemyCol;

    readonly string areaTag = "AREA";

    private void Awake()
    {
        enemyCol = GetComponent<CapsuleCollider2D>();
    }

    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag(areaTag))
            return;

        Vector3 playerPos = GameManager.instance.playerCtrl.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.playerCtrl.inputVec;

        float dirX = playerDir.x < 0 ? -1 : 1; 
        float dirY = playerDir.y < 0 ? -1 : 1; 

        switch(transform.tag)
        {
            // 시야 범위에서 벗어난 경우 재배치
            case "GROUND":
                if (diffX > diffY)
                    transform.Translate(Vector3.right * dirX * 40);
                else if(diffX < diffY)
                    transform.Translate(Vector3.up * dirY * 40);
                break;
            case "ENEMY":   // 에너미 재배치 로직
                if (enemyCol.enabled)
                {
                    transform.Translate(playerDir * 20 +
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f));
                }
                break;
        }

    }
}
