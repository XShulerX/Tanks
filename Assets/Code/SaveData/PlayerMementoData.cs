using System;

namespace MVC
{
    [Serializable]
    public class PlayerMementoData : IMomentoData
    {
        public float hp;
        public Elements element;

        public PlayerMementoData(float hp, Elements element)
        {
            this.hp = hp;
            this.element = element;
        }
    }
}