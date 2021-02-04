using UnityEngine;

namespace FrancisPasive.Platformer._2D.Character
{
    public class BetterJump : MonoBehaviour
    {
        [Range(0f, 3f)] public float m_fallMultiplier = 0f;

        [SerializeField] protected Rigidbody2D m_rigidbody;

        private void Awake()
        {
            if (m_rigidbody == null)
            {
                m_rigidbody = GetComponent<Rigidbody2D>();
                if (m_rigidbody == null)
                {
                    Debug.LogError("[Error] Missing rigidbody reference", this);
                }
            }
        }

        private void FixedUpdate()
        {
            // If falling
            if (m_rigidbody.velocity.y < 0f)
            {
                float fallMultiplier = Mathf.Clamp(m_fallMultiplier - 1f, 0f, float.MaxValue);
                m_rigidbody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.fixedDeltaTime;
            }
        }

    } // BetterJump end

} // namespace end

