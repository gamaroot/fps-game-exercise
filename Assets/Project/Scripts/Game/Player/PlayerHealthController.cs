using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace game
{
    public class PlayerHealthController : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float animationDuration = 0.5f;

        [Header("Components")]
        [SerializeField] private Slider healthBar;
        [SerializeField] private TextMeshProUGUI healthText;

        [Header("Callbacks")]
        [SerializeField] private UnityEvent onPlayerDeath;

        private int health;
        private Tween healthBarAnimation;

        private void Awake()
        {
            this.health = this.maxHealth;

            this.healthBar.minValue = 0;
            this.healthBar.maxValue = this.maxHealth;
            this.healthBar.value = this.maxHealth;
        }

        public void OnGetDamaged(int damage)
        {
            Debug.Log($"Player has been attacked! [{damage} dmg]");
            this.OnHealthUpdate(-damage);
        }

        public void OnGetHealed(int heal)
        {
            Debug.Log($"Player has been healed! [{heal} dmg]");
            this.OnHealthUpdate(heal);
        }

        private void OnHealthUpdate(int value)
        {
            if (this.health == 0)
                return;

            Debug.Log($"Health updated: {this.health} => {this.health + value}");

            this.health += value;
            if (this.health <= 0)
            {
                this.health = 0;
                Debug.Log("Player has died!");

                this.onPlayerDeath.Invoke();
            }

            void OnValueAnimate (int newHealth)
            {
                this.healthBar.value = newHealth;

                float healthPercentage = (float)newHealth / (float)this.maxHealth;
                healthPercentage *= 100f;
                this.healthText.text = $"{(int)healthPercentage}%";
            }

            this.healthBarAnimation?.Kill();
            this.healthBarAnimation = DOTween.To(() => (int)this.healthBar.value, OnValueAnimate, this.health, this.animationDuration);
        }
    }
}