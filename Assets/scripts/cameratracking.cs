using UnityEngine;

public class cameratracking : MonoBehaviour
{
    public Transform player;  // 플레이어 오브젝트
    public Vector2 offset = new Vector2(0, 0);  // 카메라 오프셋 (2D)
    public float smoothSpeed = 5f;  // 카메라 부드러운 정도

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // 2D 방식 - z축은 -10으로 고정
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -10);

        // 카메라 부드럽게 이동
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance > 2f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}

