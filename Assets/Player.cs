using UnityEngine;

public class Player : MonoBehaviour
{

    // publicとすると、Inspectorで値を変更できる。
    // 公開する理由がない限りはprivateで調整できなくしておくのが基本（誤った調整操作を防ぐ）
    private Animator anim;
    private Rigidbody2D rb;

    // set size, when you create array
    // そこまでFlexibleでないということ。 処理が比較的早く、固定スロットの要素に使うとよい。
    // スキルが4枠割り当てられる仕様で、それが固定なら[] Arrayを使う
    [Header("Attack details")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask whatIsEnemy;

    // can have dynamic size 処理が比較的遅め
    // アイテムイベントリなど、状況によって変動するものをセットする場合こちらを使うとよい
    //public List<Collider2D> exampleList; 


    [Header("Movement details")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    private float xInput;
    private bool facingRight = true;
    private bool canMove = true;
    private bool canJump = true;

    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    private void Awake()
    {
        // script状に存在するRigidbody2Dコンポーネントを割り当てる
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
    }

    public void DamageEnemies()
    {
        Collider2D[] enemyColliders;
        enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsEnemy);

        foreach (Collider2D enemy in enemyColliders)
        {
            enemy.GetComponent<Enemy>().TakeDamage();
        }
    }

    public void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
        canJump = enable;
    }

    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }


    private void HandleInput()
    {
        // GetAxis: 徐々に-1, +1に近づくようになる
        xInput = Input.GetAxisRaw("Horizontal"); // 瞬時に-1, +1に変わる

        // GetKey: 押している間 ...Down: 押した瞬間 ...Up: 離した瞬間
        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryToAttack();

    }

    private void TryToAttack()
    {
        if (isGrounded)
            anim.SetTrigger("attack");
    }

    private void TryToJump()
    {
        Debug.Log("Someone pressed Jump() key");
        if (isGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void HandleMovement()
    {
        // -1 or +1 にするだけなので、Updateでやってしまって構わない。
        if (canMove == true)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y); // 2次元ベクトル: Vector2
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // canMoveがfalse(攻撃中)の間は横移動を0にする
    }


    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void HandleFlip()
    {
        // linearVelocity.x: 右移動時は+, 左移動時は-
        // 右に移動しており, 右を向いていない場合は反転
        if (rb.linearVelocity.x > 0 && facingRight == false)
            Flip();
        else if (rb.linearVelocity.x < 0 && facingRight == true)
            Flip();
    }

    [ContextMenu("Flip")] // Inspectorのコンテキストメニューから呼び出せるようにできる
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight; // 呼ばれるたび反転させる
    }

    private void OnDrawGizmos()
    {
        // エディタ上でのみ描画するための設定
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}