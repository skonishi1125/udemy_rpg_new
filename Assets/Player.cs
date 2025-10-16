using UnityEngine;

public class Player : MonoBehaviour
{

    // publicとすると、Inspectorで値を変更できる。
    // 公開する理由がない限りはprivateで調整できなくしておくのが基本（誤った調整操作を防ぐ）
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    private float xInput;

    private void Awake()
    {
        // script状に存在するRigidbody2Dコンポーネントを割り当てる
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();

    }

    private void HandleInput()
    {
        // GetAxis: 徐々に-1, +1に近づくようになる
        xInput = Input.GetAxisRaw("Horizontal"); // 瞬時に-1, +1に変わる

        // GetKey: 押している間 ...Down: 押した瞬間 ...Up: 離した瞬間
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

    }


    private void HandleMovement()
    {
        // -1 or +1 にするだけなので、Updateでやってしまって構わない。
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y); // 2次元ベクトル: Vector2
    }

    private void Jump()
    {
        Debug.Log("Someone pressed Jump() key");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}
