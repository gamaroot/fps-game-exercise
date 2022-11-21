using System;
using UnityEngine;
using UnityEngine.AI;

namespace game
{
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class Enemy : Spawnable
    {
        [Header("Properties")]
        [SerializeField] private EnemyType enemyType;

        [Range(1f, 3f)]
        [SerializeField] private float attackDistance = 2f;

        [SerializeField] private float minDamage = 5f;
        [SerializeField] private float maxDamage = 25f;

        [Header("Components")]
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent agent;

        private IPlayer player;
        private Action onEnemyDeath;

        private void OnValidate()
        {
            if (this.animator == null)
                this.animator = base.GetComponent<Animator>();

            if (this.agent == null)
                this.agent = base.GetComponent<NavMeshAgent>();
        }

        private void Awake()
        {
            this.agent.stoppingDistance = this.attackDistance;
            this.animator.SetInteger(AnimationConstants.MOVE_VARIATION, UnityEngine.Random.Range(0, 3));
        }

        private void Update()
        {
            Vector3 playerPosition = this.player.GetPosition();

            this.animator.SetFloat(AnimationConstants.SPEED, agent.speed);

            this.agent.SetDestination(playerPosition);
            if (Vector3.Distance(base.transform.position, playerPosition) < this.attackDistance)
                Attack();
        }

        public void Setup(IPlayer player, Action onEnemyDeath)
        {
            this.player = player;
            this.onEnemyDeath = onEnemyDeath;
        }

        private void Attack()
        {
            this.animator.SetInteger(AnimationConstants.ATTACK_VARIATION, UnityEngine.Random.Range(0, 2));
            this.animator.SetTrigger(AnimationConstants.ATTACK_TRIGGER);

            this.player.OnPathBlocked();
            this.player.OnGetAttacked(UnityEngine.Random.Range(minDamage, maxDamage));
        }

        public void Death()
        {
            this.animator.SetInteger(AnimationConstants.DEATH_VARIATION, UnityEngine.Random.Range(0, 5));
            this.animator.SetTrigger(AnimationConstants.DEATH_TRIGGER);
            this.GetComponent<Collider>().enabled = false;

            this.player.OnPathUnblocked();
            this.onEnemyDeath.Invoke();

            this.agent.isStopped = true;
            this.enabled = false;
        }
    }
}