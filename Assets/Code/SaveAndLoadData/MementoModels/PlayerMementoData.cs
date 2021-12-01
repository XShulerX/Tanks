using System;

namespace MVC
{
    [Serializable]
    public class PlayerMementoData : IMementoData
    {
        public int id;
        public float hp;
        public Elements element;

        public PlayerMementoData(float hp, Elements element, int id)
        {
            this.hp = hp;
            this.element = element;
            this.id = id;
        }
    }
}