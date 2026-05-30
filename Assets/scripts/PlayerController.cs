using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rd;
    float acceleration;
    float maxspeed;
    float jumpforce;
    float lastXSpeed;
    bool isgrounded;
    bool waspressed;
    bool justJumped = false;  // 방금 점프했는지 표시
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.gravityScale = 1f;
        rd.constraints = RigidbodyConstraints2D.FreezeRotation;
        acceleration = 0.1f;
        maxspeed = 10.0f;
        jumpforce = 50f;
        isgrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        float xspeed = rd.linearVelocity.x;
        if (Keyboard.current.dKey.isPressed)
        {
            xspeed = xspeed + acceleration;
            if (xspeed > maxspeed)
            {
                xspeed = maxspeed;
            }
            rd.linearVelocity = new Vector2(xspeed, rd.linearVelocity.y);
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            xspeed = xspeed - acceleration;
            if (xspeed < -maxspeed)
            {
                xspeed = -maxspeed;
            }
            rd.linearVelocity = new Vector2(xspeed, rd.linearVelocity.y);
            lastXSpeed = xspeed;
        }
        else
        {
            xspeed = 0.95f * xspeed;
            if (xspeed < 0.05f && xspeed > -0.05f)
            {
                xspeed = 0;
            }
            rd.linearVelocity = new Vector2(xspeed, rd.linearVelocity.y);
        }

        // 점프 입력 처리
        if (Keyboard.current.wKey.isPressed && isgrounded)
        {
            lastXSpeed = rd.linearVelocity.x;  // 점프 전 x 속도 저장
            rd.linearVelocity = new Vector2(rd.linearVelocity.x, jumpforce);
            isgrounded = false;
            justJumped = true;  // 점프 플래그 설정
        }
        
        

        rd.AddForce(new Vector2(0, -10));
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 위에서 내려올 때만 (y <= 0)
            if (rd.linearVelocity.y <= 0)
            {
                isgrounded = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 위에서 내려올 때 (y <= 0)
            if (rd.linearVelocity.y <= 0)
            {
                isgrounded = true;
                if (justJumped)
                {
                    rd.linearVelocity = new Vector2(lastXSpeed, rd.linearVelocity.y);
                    justJumped = false;
                }
            }
            // 아래에서 올라올 때 (y > 0) - 천장이므로 붙지 않게
            else
            {
                rd.linearVelocity = new Vector2(rd.linearVelocity.x, -0.1f);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = false;
        }
    }
}
