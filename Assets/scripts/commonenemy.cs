using UnityEngine;

public class commonenemy : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rd;
    private Transform playerTransform;
    private float acceleration;
    private float maxspeed;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        acceleration = 5f;  // 0.1f → 5f로 증가
        maxspeed = 7.0f;
    }

    void Update()
    {
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (transform.position.x > playerTransform.position.x)
        {
            rd.AddForce(new Vector2(-acceleration, 0));
        }
        else if (transform.position.x < playerTransform.position.x)
        {
            rd.AddForce(new Vector2(acceleration, 0));
        }
        if (rd.linearVelocity.x > maxspeed)
        {
            rd.linearVelocity = new Vector2(maxspeed, rd.linearVelocity.y);
        }
        else if (rd.linearVelocity.x < -maxspeed)
        {
            rd.linearVelocity = new Vector2(-maxspeed, rd.linearVelocity.y);
        }
        rd.AddForce(new Vector2(0, -10f));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌!");
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {

        }
        else
        {
            rd.linearVelocity = new Vector2(rd.linearVelocity.x, 40f);
        }
    }
}

