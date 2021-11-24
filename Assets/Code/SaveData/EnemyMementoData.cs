using System;

namespace MVC
{
    [Serializable]
    public class EnemyMementoData: IMomentoData
    {
        public int id;
        public float maxHP;
        public float curentHP;
        public Elements element;

        public EnemyMementoData(int id, float maxHP, float curentHP, Elements element)
        {
            this.id = id;
            this.maxHP = maxHP;
            this.curentHP = curentHP;
            this.element = element;
        }
    }
}