using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityInput = UnityEngine.Input;

namespace FrancisPasive.Platformer._2D.Character.Input
{
    public class Platformer2DUserInputController : MonoBehaviour
    {
        private PlatformerCharacter2D m_character;
        private float m_horizontalMove = 0f;
        private bool m_jump = false;
        private bool m_crouch = false;

        private void Awake()
        {
            m_character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
            m_jump = UnityInput.GetButtonDown("Jump");
            m_crouch = UnityInput.GetButton("Crouch");
            m_horizontalMove = UnityInput.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            m_character.Move(m_horizontalMove, m_crouch, m_jump);
            m_jump = false;
        }

    } // Platformer2DUserInputController end

} // namespace end

