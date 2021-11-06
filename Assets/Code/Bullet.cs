using UnityEngine;

namespace MVC
{
    public class Bullet : MonoBehaviour
    {
        public int Damage;

        [HideInInspector] public GameObject GetCollisionObject;

        private Transform _container;


        public void OnCollisionEnter(Collision collision)
        {
            GetCollisionObject = collision.gameObject;
        }


        public void InvokeTimer()
        {
            Invoke(nameof(TimeToGoBackInPool), 4f);
        }

        public void SetContainer(Transform container)
        {
            _container = container;
        }

        public void TimeToGoBackInPool()
        {
            transform.position = _container.transform.position;
            gameObject.SetActive(false);
        }
    }
}
