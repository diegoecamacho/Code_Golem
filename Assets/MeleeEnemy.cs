using UnityEngine;
using UnityEngine.AI;
using CodeGolem.Actor;
using CodeGolem.Combat;

namespace CodeGolem.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MeleeEnemy : EnemyBase
    {
        [Header("Entity Stats")]
        EnemyParams inputParams;
        NavMeshAgent agent;

        [Header("State Variables")]
        [SerializeField] bool onAlert;

        [Header("Weapon")]
        private float weaponRange = 1f;

        [Header("Debug")]
        public Transform playerTransform;
        public bool Move = false;
        public EnemyStates state;
        private float deadZone;
       

        // Use this for initialization
        void Start()
        {
            inputParams = new EnemyParams
            {
                destination = playerTransform
            };
            agent = GetComponent<NavMeshAgent>();

        }

        // Update is called once per frame
        void Update()
        {

            Debug.Log(Vector3.Distance(transform.position, inputParams.destination.position));
            switch (enemyState)
            {
                case EnemyStates.Idle:
                   //if (Vector3.Distance(transform.position, stateParams.destination.position) > approachRange)
                   //{
                      enemyState = EnemyStates.Move;
                      agent.SetDestination(inputParams.destination.position);
                   //}
                    return;
                case EnemyStates.Move:
                    if (onAlert && Vector3.Distance(transform.position, inputParams.destination.position) < weaponRange)
                    {
                        Attack();
                    }
                    else if(Vector3.Distance(transform.position, inputParams.destination.position) < approachRange){
                        Debug.Log("lESS THAN aPP");
                        agent.isStopped = true;
                        enemyState = EnemyStates.Idle;
                    }
                    
                    break;
                case EnemyStates.Attack:
                    break;
                case EnemyStates.Dead:
                    break;
                default:
                    break;
            }

        }

        private void ActorMove()
        {
            if (transform.position == inputParams.destination.position)
            {
                return;
            }
            else
            {
                agent.SetDestination(inputParams.destination.position);
            }    
        }

        public override void Attack()
        {
            inputParams.destination.gameObject.GetComponent<IDamageable>().TakeDamage(ActorStats.GetDamage());
            onAlert = false;
            enemyState = EnemyStates.Idle;
        }

        public override void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }

        public override void Interact()
        {
            throw new System.NotImplementedException();
        }
    }
}
