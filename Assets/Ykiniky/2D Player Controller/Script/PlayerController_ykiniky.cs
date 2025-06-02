using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YkinikY
{
    public class PlayerController_ykiniky : MonoBehaviour
    {
        [Header("(c) Ykiniky")]
        [Header("Movement")]
        public bool canMove = true;
        public bool canJump = true;
        public float velocity = 1;

        [Header("Camera")]
        public PlayerCameraFollow_ykiniky playerCameraFollow;
        public Vector2 lastCheckpoint;

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;

        private bool isDead = false;

        private int groundContacts = 0;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (!isDead)
            {
                if (canMove) MovimentUpdate();

                if (playerCameraFollow != null)
                {
                    if (transform.position.x > 0)
                        playerCameraFollow.FollowX();
                    else
                        playerCameraFollow.DontFollowX();

                    if (transform.position.y > 0)
                        playerCameraFollow.FollowY();
                    else
                        playerCameraFollow.DontFollowY();
                }
            }
        }

        void MovimentUpdate()
        {
            float moveInput = Input.GetAxisRaw("Horizontal");

            if (moveInput != 0)
            {
                transform.position += moveInput * velocity * Time.deltaTime * Vector3.right;
                spriteRenderer.flipX = (moveInput < 0);
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump")) && canJump)
            {
                canJump = false;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 6f);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
           
            if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Platform"))
            {
                groundContacts++;
                canJump = true;
            }

            if (collision.gameObject.CompareTag("Hoyo"))
            {
                Death();
                StartCoroutine(RestartLevel());
            }



            if (collision.gameObject.CompareTag("Enemy"))
            {
                bool playerTouchedFromAbove = false;

                foreach (ContactPoint2D contact in collision.contacts)
                {
                   
                    if (contact.normal.y > 0.5f)
                    {
                        playerTouchedFromAbove = true;
                        break;
                    }
                }

               

                if (!playerTouchedFromAbove)
                {

                    Death();

                }

                
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Platform"))
            {
                groundContacts--;
                if (groundContacts <= 0)
                {
                    groundContacts = 0;
                    canJump = false;
                }
            }
        }

    
        void Death()
        {
            if (isDead) return;

            isDead = true;
            canMove = false;
            rb.linearVelocity = Vector2.zero;

            Debug.Log("¡El personaje ha muerto!");

            StartCoroutine(RestartLevel());
        }

        IEnumerator RestartLevel()
        {
            yield return new WaitForSeconds(2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
