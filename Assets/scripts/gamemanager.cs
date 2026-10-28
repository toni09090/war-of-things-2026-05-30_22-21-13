using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 namespace

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 패턴 (선택 사항)

    public GameObject panel; // 게임오버 패널 연결
    public bool isGameOver = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false); // 게임 시작 시 게임오버 UI 비활성화
        }
    }
    public void GameOver()
    {
        isGameOver = true;
        if (panel != null)
        {
            panel.SetActive(true); // 게임오버 UI 활성화
        }
    }

    public void RestartGame()
    {
        // 현재 활성화된 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
