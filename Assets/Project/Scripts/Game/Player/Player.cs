using UnityEngine;

namespace game
{
    [RequireComponent(typeof(PlayerController))]
    public class Player : MonoBehaviour, IPlayer
    {
        [Header("Components")]
        [SerializeField] private PlayerController controller;

        private float m_health = 100f;

        private void OnValidate()
        {
            if (controller == null)
                this.controller = base.GetComponent<PlayerController>();
        }

        public void OnGetAttacked(float damage)
        {
            this.m_health -= damage;

            if (this.m_health <= 0f)
                this.Die();
        }

        public void OnPathBlocked()
        {
            this.controller.ToggleMovement(false);
        }

        public void OnPathUnblocked()
        {
            this.controller.ToggleMovement(true);
        }

        public Vector3 GetPosition()
        {
            return base.transform.position;
        }

        private void Die()
        {
            Debug.Log("Player has Died!");
            base.enabled = false;
        }
    }
}