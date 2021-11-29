using System;

namespace MVC
{
    [Serializable]
    public class EnemyMementoData: IMementoData
    {
        public int id;
        public float hp;
        public float maxHP;
        public bool isFly;
        public Elements element;

        public EnemyMementoData(int id, float hp, float maxHP, bool isFly, Elements element)
        {
            this.id = id;
            this.hp = hp;
            this.maxHP = maxHP;
            this.isFly = isFly;
            this.element = element;
        }
    }
}