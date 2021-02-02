using UnityEngine;
using UnityEngine.Events;
using System;

namespace FrancisPasive.Core.Health
{
    [Serializable]
    public class Damageable
    {
        public float maxHealth;
        public float startingHealth;
        public float currentHealth { get; private set; }
        public bool isDead => currentHealth <= 0f;
        public bool isAtMaxHealth => Mathf.Approximately(currentHealth, maxHealth);

        // Events
        public UnityAction reachedMaxHealth;

        public virtual void Init()
        {
            currentHealth = startingHealth;
        }

        public void SetHealth(float health)
        {
            currentHealth = health;
            // TODO: Invoke health change action
        }

        public bool TakeDamage(float damageAmount)
        {

            if (isDead)
            {
                return false;
            }

            ChangeHealth(-damageAmount);

            // TODO: Invoke damaged action
            // TODO: Invoke died action if dead
            return true;
        }

        protected void ChangeHealth(float healthIncreament)
        {
            currentHealth += healthIncreament;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            // TODO: Invoke health change action
        }

    } // Damageable end

} // namespace end

