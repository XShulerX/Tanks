using UnityEngine;

namespace MVC
{
    public interface IEnemy : IGamer, IPlayerTarget
    {
        void Fire(Transform target);
        public Enemy SetPool(BulletPool pool);
        public void SetDamageModifer(float modifer);
        public void ShowOrHideCircle(bool isChoisen);
        public void Reset(float forceModifer, bool isPlayerWin);
        public int Id { get; }
        public float MaxHP { get; }
    }
}