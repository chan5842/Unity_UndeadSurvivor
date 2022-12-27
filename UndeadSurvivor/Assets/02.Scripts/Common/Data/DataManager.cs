using System.Collections;
using System.Collections.Generic;

namespace DataInfo
{
    [System.Serializable]       // 직렬화(인스펙터 창에서 보이지 않는것을 볼 수 있게 함)
    public class SpawnData
    {
        public int spriteType;  // 몬스터 종류
        public float spawnTime; // 스폰 간격
        public int hp;          // 몬스터의 체력
        public float moveSpeed; // 몬스터의 이동속도
        public int damage;      // 몬스터의 공격력
    }

    [System.Serializable]
    public class Item
    {
        public enum ItemType   // 아이템 종류
        {
            EXP,
            HEAL,
            MAGNET
        }

        public int ID;          // 고유번호
        public ItemType type;   // 종류
        public string name;     // 이름        
        public float value;     // 값(경험치 +1, 마그넷 범위, 힐 회복량 등)

    }
}
