using UnityEngine;

namespace FrancisPasive.Platformer._2D.Core.Utilities
{
    public class Parallax : MonoBehaviour
    {
        public Camera targetCamera;
        public Transform targetSubject;

        private Vector2 m_startPosition = Vector2.zero;
        private float m_startZ = 0f;

        private Vector2 m_travel => (Vector2)targetCamera.transform.position - m_startPosition;
        private float m_distanceFromSubject => m_transform.position.z - targetSubject.position.z;
        private float m_clippingPlane => targetCamera.transform.position.z + (m_distanceFromSubject > 0f ? targetCamera.farClipPlane : targetCamera.nearClipPlane);
        private float m_parallaxFactor => Mathf.Abs(m_distanceFromSubject) / m_clippingPlane;

        protected Transform m_transform;

        private void Awake()
        {
            m_transform = transform;
        }

        private void Start()
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }

            m_startPosition = m_transform.position;
            m_startZ = m_transform.position.z;
        }

        private void FixedUpdate()
        {
            Vector2 newPosition = m_startPosition + (m_travel * m_parallaxFactor);
            m_transform.position = new Vector3(newPosition.x, newPosition.y, m_startZ);
        }

    } // Parallax end

} // namespace end
