using UnityEngine;


    public class WorldCanvasPositionSetter : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0, 2f, 0);

        private void LateUpdate()
        {
            if (target == null) return;

            // Hedefin konumuna offset uygula
            transform.position = target.position + offset;

            // Kameraya dön
            if (Camera.main != null)
            {
                transform.forward = Camera.main.transform.forward;
            }
        }
    }
