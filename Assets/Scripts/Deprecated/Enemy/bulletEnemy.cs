using CodeGolem.Actor;
using CodeGolem.Level;
using UnityEngine;
using CodeGolem.Player;

namespace CodeGolem.Enemy
{
    public class bulletEnemy : MonoBehaviour, IDamageable
    {
        private ActorStats stats;

        [Header("Behavior Variables")]
        private bool IsActive = false;

        [SerializeField] private Transform destination;

        [SerializeField] private float damage;
        [SerializeField] private float speed;
        [SerializeField] private float timeToExplode;


        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }

        private void Update()
        {
            Vector3 dir = destination.position - transform.position;
            transform.Translate(dir.normalized * Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, destination.position) < 1)
            {
                Destroy(gameObject, timeToExplode);
            }  
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision");
            if (collision.transform.CompareTag("Bullet"))
            {
                LevelManager.Player.GetStats().Experience++;
                Destroy(this.gameObject , this.timeToExplode);
            }

            if (collision.transform.CompareTag("Player"))
            {
                Debug.Log("Player Hit");
                collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);

                Destroy(this.gameObject);

            }
        }
    }
}