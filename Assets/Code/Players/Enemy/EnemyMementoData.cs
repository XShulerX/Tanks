using System;

namespace MVC
{
    [Serializable]
    public class EnemyMementoData: IMomentoData
    {
        public int id;
        public float hp;
        public Elements element;

        public EnemyMementoData(int id, float hp, Elements element)
        {
            this.id = id;
            this.hp = hp;
            this.element = element;
        }
    }
}