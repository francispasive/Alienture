using UnityEngine;
using UnityEngine.Events;

namespace FrancisPasive.Core.Health
{
    public class DamageableBehaviour : MonoBehaviour
    {
        public Damageable configuration;
        public bool isDead => configuration.isDead;
        public virtual Vector3 position => transform.position;

        // Events
        public UnityAction<float> Hit;
        public UnityAction Died;

        public virtual void TakeDamage(float damageAmount, Vector3 damagePoint)
        {
            configuration.TakeDamage(damageAmount);
            if (Hit != null)
            {
                Hit(damageAmount);
            }
        }

        protected virtual void Awake()
        {
            configuration.Init();
        }

        protected virtual void Kill()
        {
            configuration.TakeDamage(configuration.currentHealth);
        }

    } // DamageableBehaviour end

} // namespace end

