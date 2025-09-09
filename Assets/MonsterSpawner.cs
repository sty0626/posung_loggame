using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // ������ ���� ������
    public float spawnInterval = 3f; // �� �ʸ��� ��������

    private Camera mainCamera;
    private float timer;

    void Start()
    {
        mainCamera = Camera.main;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnMonsterAtRandomScreenPosition();
            timer = 0f;
        }
    }

    void SpawnMonsterAtRandomScreenPosition()
    {
        // ȭ���� ������ Viewport ��ǥ (0~1)
        float randomX = Random.Range(0.1f, 0.9f);
        float randomY = Random.Range(0.1f, 0.9f);

        Vector3 viewportPos = new Vector3(randomX, randomY, 0);

        // Viewport -> World
        Vector3 worldPos = mainCamera.ViewportToWorldPoint(new Vector3(viewportPos.x, viewportPos.y, 10f));

        Instantiate(monsterPrefab, worldPos, Quaternion.identity);
        Debug.Log("���� ����! ��ġ: " + worldPos);
    }
}
