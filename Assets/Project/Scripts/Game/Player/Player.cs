using UnityEngine;

namespace game
{
    [RequireComponent(typeof(PlayerController), typeof(PlayerHealthController))]
    public class Player : MonoBehaviour, IPlayer
    {
        [Header("Components")]
        [SerializeField] private PlayerController controller;
        [SerializeField] private PlayerHealthController health;

        private void OnValidate()
        {
            if (this.controller == null)
                this.controller = base.GetComponent<PlayerController>();

            if (this.health == null)
                this.health = base.GetComponent<PlayerHealthController>();
        }

        public void OnGetAttacked(int damage)
        {
            if (!base.isActiveAndEnabled)
                return;

            this.health.OnGetDamaged(damage);
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

        // Called through PlayerHealthController's inspector
        public void OnPlayerDeath()
        {
            base.enabled = false;
        }
    }
}