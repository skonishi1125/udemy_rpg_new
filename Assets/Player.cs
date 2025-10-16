using UnityEngine;

public class Player : MonoBehaviour
{

    // public�Ƃ���ƁAInspector�Œl��ύX�ł���B
    // ���J���闝�R���Ȃ������private�Œ����ł��Ȃ����Ă����̂���{�i��������������h���j
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    private float xInput;

    private void Awake()
    {
        // script��ɑ��݂���Rigidbody2D�R���|�[�l���g�����蓖�Ă�
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();

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
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}
