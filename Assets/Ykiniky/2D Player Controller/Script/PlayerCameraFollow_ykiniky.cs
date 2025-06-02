using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YkinikY
{
    public class PlayerCameraFollow_ykiniky : MonoBehaviour
    {
        [Header("(c) Ykiniky")]
        public bool followX = true;
        public bool followY = true;
        public bool zoomed = true;

        [Header("Camera Limits")]
        public float minX = 0f;
        public float maxX = 0f;
        public float minY = 0f;
        public float maxY = 0f;

        private Camera cam;
        private Transform player;

        void Start()
        {
            cam = GetComponent<Camera>();
            player = FindAnyObjectByType<PlayerController_ykiniky>().transform;
        }

        void Update()
        {
            if (player == null) return;

          
            Vector2 targetPosition = Vector2.Lerp(transform.position, player.position, 0.5f);

          
            float clampedX = followX ? Mathf.Clamp(targetPosition.x, minX, maxX) : transform.position.x;
            float clampedY = followY ? Mathf.Clamp(targetPosition.y, minY, maxY) : transform.position.y;

           
            transform.position = new Vector3(clampedX, clampedY, -10f); 
        
            float targetZoom = zoomed ? 6f : 6f;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, 0.03f);
        }

        // Métodos públicos para controlar el comportamiento
        public void FollowX() => followX = true;
        public void FollowY() => followY = true;
        public void DontFollowX() => followX = false;
        public void DontFollowY() => followY = false;
        public void Zoom() => zoomed = true;
        public void Unzoom() => zoomed = false;
    }
}
