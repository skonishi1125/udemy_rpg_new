using UnityEngine;

public class Player : MonoBehaviour
{

    // public�Ƃ���ƁAInspector�Œl��ύX�ł���B
    // ���J���闝�R���Ȃ������private�Œ����ł��Ȃ����Ă����̂���{�i��������������h���j

    [Header ("Movement details")]
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    private float xInput;
    private bool facingRight = true;

    [Header ("Collision details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    private void Awake()
    {
        // script��ɑ��݂���Rigidbody2D�R���|�[�l���g�����蓖�Ă�
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

    private void HandleAnimations()
    {
        bool isMoving = rb.linearVelocity.x != 0;

        anim.SetBool("isMoving", isMoving);

    }


    private void HandleInput()
    {
        // GetAxis: ���X��-1, +1�ɋ߂Â��悤�ɂȂ�
        xInput = Input.GetAxisRaw("Horizontal"); // �u����-1, +1�ɕς��

        // GetKey: �����Ă���� ...Down: �������u�� ...Up: �������u��
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

    }

    private void HandleMovement()
    {
        // -1 or +1 �ɂ��邾���Ȃ̂ŁAUpdate�ł���Ă��܂��č\��Ȃ��B
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y); // 2�����x�N�g��: Vector2
    }

    private void Jump()
    {
        Debug.Log("Someone pressed Jump() key");
        if (isGrounded) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void HandleFlip()
    {
        // linearVelocity.x: �E�ړ�����+, ���ړ�����-
        // �E�Ɉړ����Ă���, �E�������Ă��Ȃ��ꍇ�͔��]
        if (rb.linearVelocity.x > 0 && facingRight == false)
            Flip();
        else if (rb.linearVelocity.x < 0 && facingRight == true)
            Flip();
    }

    [ContextMenu("Flip")] // Inspector�̃R���e�L�X�g���j���[����Ăяo����悤�ɂł���
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight; // �Ă΂�邽�є��]������
    }

    private void OnDrawGizmos()
    {
        // �G�f�B�^��ł̂ݕ`�悷�邽�߂̐ݒ�
        // 
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }
}