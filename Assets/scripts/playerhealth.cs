using UnityEngine;
using UnityEngine.InputSystem;

public class playerhealth : MonoBehaviour
{
    public int maxHealth = 100;  // 최대 체력
    public int currentHealth;     // 현재 체력
    public GameManager gm;
    void Start()
    {
        currentHealth = maxHealth;  // 시작 시 최대 체력으로 설정
        Debug.Log("플레이어 체력: " + currentHealth + "/" + maxHealth);
        gm = GameManager.instance;
    }

    void Update()
    {

    }

    // 데미지를 받는 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("데미지! 현재 체력: " + currentHealth);

        // 체력이 0 이하면 죽음
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    // 회복하는 함수
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("회복! 현재 체력: " + currentHealth);
    }

    // 플레이어가 죽는 함수
    void Die()
    {
        Debug.Log("플레이어 사망!");
        gm.GameOver();  // 게임오버 처리
    }

    // 현재 체력 반환
    public int GetHealth()
    {
        return currentHealth;
    }

    // 체력 퍼센트 반환 (UI용)
    public float GetHealthPercent()
    {
        return (float)currentHealth / maxHealth;
    }
}

