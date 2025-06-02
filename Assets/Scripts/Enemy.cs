using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 100f;
    public float speed = 5.0f;

    private Rigidbody2D rb;
    private Vector2 movement;


    public GameObject EnemyCollider;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            movement = new Vector2(direction.x, 0);
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float yOffset = 0.4f;
            if (transform.position.y + yOffset < collision.transform.position.y)
            {
                player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * 7;
                EnemyCollider.GetComponent<CapsuleCollider2D>().isTrigger = false;
                Invoke("Death",0.1f);
            }
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
    


}

