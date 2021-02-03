using UnityEngine;

namespace FrancisPasive.Platformer._2D.Character
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private bool m_isFacingRight = true;

        [SerializeField] private float m_maxMoveSpeed = 10f;
        [SerializeField] private float m_jumpForce = 200f;
        private bool m_isOnAir = false;

        [Range(0, 1f)] [SerializeField] private float m_crouchSpeed = .36f;

        [SerializeField] private bool m_airControl = true; // Whether or not the character can steer while jumping/on air.

        // Ground Check Properties
        [SerializeField] protected LayerMask m_groundMask;
        [SerializeField] protected Transform m_groundCheck; // A position marking where to check if the character is grounded.
        [SerializeField] private float m_groundRadius = .2f; // Radius of the overlap circle to determine if grounded.
        public bool isGrounded { get; private set; }

        // Ceiling Check Properties
        [SerializeField] protected Transform m_ceilingCheck; // A position marking where to check if the character is grounded.
        [SerializeField] private float m_ceilingRadius = .2f; // Radius of the overlap circle to determine if grounded.

        // Ceiling Check Properties
        [SerializeField] protected Transform m_wallCheck; // A position marking where to check if the character is facing wall.
        [SerializeField] private float m_wallRadius = .2f; // Radius of the overlap circle to determine if facing wall.

        [SerializeField] protected Transform m_transform;
        [SerializeField] protected Rigidbody2D m_rigidbody;
        [SerializeField] protected Animator m_animator;
        private readonly string ANIMATOR_CROUCH = "isCrouching";
        private readonly string ANIMATOR_SPEED = "speed";
        private readonly string ANIMATOR_JUMP = "jumpTrigger";
        private readonly string ANIMATOR_ON_AIR = "isOnAir";
        private readonly string ANIMATOR_FALL = "isFalling";

        private void Awake()
        {
            if (m_transform == null)
            {
                m_transform = transform;
            }

        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(m_groundCheck.position, m_groundRadius, m_groundMask);

            // Character landing
            if (m_isOnAir && isGrounded)
            {
                Debug.Log("Landed");
            }

            // Check if character is on air
            m_isOnAir = !isGrounded;
            m_animator.SetBool(ANIMATOR_ON_AIR, m_isOnAir);

            // Check if character is falling
            bool isFalling = m_rigidbody.velocity.y < 0f;
            m_animator.SetBool(ANIMATOR_FALL, isFalling);
        }

        public void Move(float horizontalMove, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_animator.GetBool(ANIMATOR_CROUCH))
            {
                // If the character has a ceiling preventing them to stand up, keep crouching
                if (Physics2D.OverlapCircle(m_ceilingCheck.position, m_ceilingRadius, m_groundMask))
                {
                    crouch = true;
                }
            }
            // If crouching, check to see if the character is on ground or not
            else if (crouch && !isGrounded)
            {
                crouch = false;
            }

            m_animator.SetBool(ANIMATOR_CROUCH, crouch);

            if (isGrounded || m_airControl)
            {
                // Reduce the speed if crouching by the crouch speed multiplier
                horizontalMove = crouch ? horizontalMove * m_crouchSpeed : horizontalMove;

                m_animator.SetFloat(ANIMATOR_SPEED, Mathf.Abs(horizontalMove));

                // Move the character
                m_rigidbody.velocity = new Vector2(horizontalMove * m_maxMoveSpeed, m_rigidbody.velocity.y);

                // Face character to move direction
                if ((horizontalMove > 0 && !m_isFacingRight) || (horizontalMove < 0 && m_isFacingRight))
                {
                    Flip();
                }

            }

            if (jump && isGrounded)
            {
                isGrounded = false;
                m_animator.SetTrigger(ANIMATOR_JUMP);
                m_rigidbody.AddForce(Vector2.up * m_jumpForce);

            }
        }

        private void Flip()
        {
            m_isFacingRight = !m_isFacingRight;
            m_transform.Rotate(0f, 180f, 0f);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Ground check visualizer
            if (m_groundCheck != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(m_groundCheck.position, m_groundRadius);
            }

            // Ceiling check visualizer
            if (m_ceilingCheck != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(m_ceilingCheck.position, m_ceilingRadius);
            }

            // Ceiling check visualizer
            if (m_wallCheck != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(m_wallCheck.position, m_wallRadius);
            }
        }
#endif
    } // PlatformerCharacter2D end

} // namespace end
