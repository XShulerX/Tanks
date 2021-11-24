using UnityEngine;

namespace MVC
{
    public class Bullet : MonoBehaviour
    {
        public float Damage;
        public Elements element;

        [HideInInspector] public GameObject GetCollisionObject;

        private Transform _container;
        private Rigidbody _rigidbody;

        public const float DEFAULT_DAMAGE = 5;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();  
        }

        public void OnCollisionEnter(Collision collision)
        {
            GetCollisionObject = collision.gameObject;
        }


        public void InvokeTimer()
        {
            Invoke(nameof(TimeToGoBackInPool), 2f);
        }

        public void SetContainer(Transform container)
        {
            _container = container;
        }

        public void TimeToGoBackInPool()
        {
            Damage = DEFAULT_DAMAGE;
            _rigidbody.Sleep();
            transform.position = _container.transform.position;
            transform.rotation = _container.transform.rotation;
            gameObject.SetActive(false);
        }
    }
}
