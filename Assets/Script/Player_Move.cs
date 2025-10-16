using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Player_Move : MonoBehaviour
{
    [Header("�v���C���[�ݒ�")]�@�@�@                          
    [Tooltip("��]������͂̑傫��")]�@�@�@�@�@�@�@�@�@�@�@�@ 
    [SerializeField] private float rotationTorque = 20000f;�@ 

    [Tooltip("��]�ɉ��������x�㏸��")]
    [SerializeField] private float speedFactor = 1f;

    [Tooltip("�W�����v��")]
    [SerializeField] private float Jumpforce = 3f;

    [Tooltip("���C�́i�ʏ�j")]
    [SerializeField] public PhysicsMaterial normalMat;

    [Tooltip("���C�́i�ړ����j")]
    [SerializeField] public PhysicsMaterial MovingMat;

    private Collider col;
    private Rigidbody rb;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        //��]��Y���̂ݐ���
        rb.constraints = RigidbodyConstraints.FreezePositionZ |
        RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        HandleInput();
    }

    /// <summary>
    /// ���͂��󂯎��e�������Ăяo��
    /// </summary>
    void HandleInput()
    {

        float rotationInput = 0f;
        // D�L�[ �� �E��]���E�����։���
        if (Input.GetKey(KeyCode.D)) rotationInput = -1f;
        // A�L�[ �� ����]{���������։���
        if (Input.GetKey(KeyCode.A)) rotationInput = 1f;
        // SPACE�L�[ �� �W�����v����
        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            Jump();
        }

        if (rotationInput != 0)
        {
            col.material = MovingMat;
            Rotate(rotationInput);
            Move(rotationInput);
        }
        else
        {
            col.material = normalMat;
        }
    }

    /// <summary>
    /// ��]����
    /// </summary>
    private void Rotate(float input)
    {
        //y�����̉�]
        float currentAngularSpeed = Mathf.Abs(rb.angularVelocity.y);
        //��]�𐶂ݏo���́~��]�����~�i1 + ��]�X�s�[�h)
        float torque = rotationTorque * Mathf.Sign(input) * (1 + Mathf.Abs(currentAngularSpeed));

        rb.AddTorque(transform.up * torque, ForceMode.Force);
    }

    /// <summary>
    /// ��]���x�ɉ������O�i����
    /// </summary>
    private void Move(float input)
    {
        // �v���C���[�́u�����ڂ̉E�����v�����߂�
        float angularSpeed = Mathf.Abs(rb.angularVelocity.y);
        float moveForce = 1 + angularSpeed * speedFactor;
        Vector3 moveDir = transform.right * input;
        rb.AddForce(moveDir * moveForce, ForceMode.Force);
    }

    /// <summary>
    /// �W�����v����
    /// </summary>
    private void Jump()
    {
        rb.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
        isJumping = true;
    }

    /// <summary>
    ///���n����
    /// </summary>

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

}