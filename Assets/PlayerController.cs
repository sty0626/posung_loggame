using UnityEngine;  // Unity의 기본 기능들을 사용하기 위해 포함

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;  // 플레이어 이동 속도를 조절하는 변수 (public이므로 인스펙터에서 설정 가능
    public int maxHP = 10;        // 플레이어 최대 체력 설정

    private int currentHP;        // 플레이어 현재 체력
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();  // 이 오브젝트에 붙어 있는 Rigidbody2D를 가져옴
    }

    // 매 프레임마다 실행되는 함수 (키 입력 등 사용자 입력 처리에 사용)
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // 고정된 시간 간격마다 실행되는 함수 (물리 연산은 여기서 처리)
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log("플레이어가 공격당함! 남은 체력: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("플레이어 사망");
    }
}
