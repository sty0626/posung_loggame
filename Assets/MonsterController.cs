using System.Collections;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float attackRange = 1.2f;
    public int maxHP = 3;
    public float attackCooldown = 1f;

    private int currentHP;
    private Transform player;
    private Rigidbody2D rb;
    private float lastAttackTime = -999f;
    private PlayerController playerController;

    private bool hasDetectedPlayer = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        currentHP = maxHP;

        // 플레이어 찾기
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.transform;
        playerController = playerObj.GetComponent<PlayerController>();

        rb = GetComponent<Rigidbody2D>();

        // 스프라이트 색상 기억
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // 처음 탐지되면 추적 시작
        if (!hasDetectedPlayer && distance <= detectionRange)
        {
            hasDetectedPlayer = true;
            Debug.Log("플레이어 발견!");
        }

        // 추적 상태라면 계속 따라가기
        if (hasDetectedPlayer)
        {
            MoveTowardPlayer();

            if (distance <= attackRange)
            {
                AttackPlayer();
            }
        }
    }

    void MoveTowardPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            playerController.TakeDamage(1);
            lastAttackTime = Time.time;
            Debug.Log("몬스터가 플레이어를 공격함!");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        // 피격 색상 효과
        StartCoroutine(FlashRed());

        if (currentHP <= 0)
        {
            Die();
        }

        Debug.Log("몬스터가 공격당함! 남은 체력: " + currentHP);
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.7f);
        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        Debug.Log("몬스터 사망");
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Kickable"))
        {
            TakeDamage(1);
            Debug.Log("몬스터가 공에 맞음!");
        }
    }
}
