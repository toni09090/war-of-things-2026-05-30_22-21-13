using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    // 1단계에서 만든 적 프리팹을 넣을 변수
    public GameObject enemyPrefab;

    // 적이 생성될 위치 (지정하지 않으면 0,0,0 위치에 생성)
    public Transform spawnPoint;

    // 버튼을 눌렀을 때 호출할 함수
    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (enemyPrefab == null)
            {
                Debug.LogError("적 프리팹이 연결되지 않았습니다!");
                return;
            }

            // 생성 위치 결정 (spawnPoint가 없으면 기본 위치)
            Vector2 position = spawnPoint != null ? spawnPoint.position : Vector2.zero;
            Quaternion rotation = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

            // Instantiate는 유니티에서 오브젝트를 복제(생성)하는 함수입니다.
            Instantiate(enemyPrefab, position, rotation);
        }
    }
}