using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rd;
    float acceleration;
    float maxspeed;
    float mayspeed;
    float jumpforce;
    float timer; //반천장 위에서 점프 가능하게 하기 위한 타이머
    bool isgrounded = false;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.gravityScale = 1f;
        rd.constraints = RigidbodyConstraints2D.FreezeRotation;
        acceleration = 0.1f;
        maxspeed = 10.0f;
        mayspeed = -30.0f;
        jumpforce = 40f;
        timer = 0f;
        isgrounded = false;
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
        if (timer > 0.05f)
        {
            isgrounded = true;
        }
        // 점프 입력 처리
        if (Keyboard.current.wKey.isPressed && isgrounded)
        {
            rd.linearVelocity = new Vector2(rd.linearVelocity.x, jumpforce);
            isgrounded = false;
        }
        
        
        if (!(isgrounded))
        {
            rd.AddForce(new Vector2(0, -10));
            if (mayspeed > rd.linearVelocity.y)
            {
                rd.linearVelocity = new Vector2(rd.linearVelocity.x, mayspeed);
            }
        }
        else
        {
            rd.linearVelocity = new Vector2(rd.linearVelocity.x, 0);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = true;
        }
        if (collision.gameObject.CompareTag("cilling"))
        {
            timer += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = true;
        }
        else if (collision.gameObject.CompareTag("cilling"))
        {
            rd.transform.position = new Vector2(rd.transform.position.x, rd.transform.position.y - 0.1f);
            rd.linearVelocity = new Vector2(rd.linearVelocity.x, 0);
            timer = 0f;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("cilling"))
        {
            isgrounded = false;
            timer = 0f;
        }
    }
}
