using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Player_Move : MonoBehaviour
{
    [Header("プレイヤー設定")]　　　                          
    [Tooltip("回転させる力の大きさ")]　　　　　　　　　　　　 
    [SerializeField] private float rotationTorque = 20000f;　 

    [Tooltip("回転に応じた速度上昇率")]
    [SerializeField] private float speedFactor = 1f;

    [Tooltip("ジャンプ力")]
    [SerializeField] private float Jumpforce = 3f;

    [Tooltip("摩擦力（通常）")]
    [SerializeField] public PhysicsMaterial normalMat;

    [Tooltip("摩擦力（移動時）")]
    [SerializeField] public PhysicsMaterial MovingMat;

    private Collider col;
    private Rigidbody rb;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        //回転はY軸のみ制限
        rb.constraints = RigidbodyConstraints.FreezePositionZ |
        RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        HandleInput();
    }

    /// <summary>
    /// 入力を受け取り各処理を呼び出す
    /// </summary>
    void HandleInput()
    {

        float rotationInput = 0f;
        // Dキー → 右回転＆右方向へ加速
        if (Input.GetKey(KeyCode.D)) rotationInput = -1f;
        // Aキー → 左回転{＆左方向へ加速
        if (Input.GetKey(KeyCode.A)) rotationInput = 1f;
        // SPACEキー → ジャンプする
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
    /// 回転処理
    /// </summary>
    private void Rotate(float input)
    {
        //y方向の回転
        float currentAngularSpeed = Mathf.Abs(rb.angularVelocity.y);
        //回転を生み出す力×回転方向×（1 + 回転スピード)
        float torque = rotationTorque * Mathf.Sign(input) * (1 + Mathf.Abs(currentAngularSpeed));

        rb.AddTorque(transform.up * torque, ForceMode.Force);
    }

    /// <summary>
    /// 回転速度に応じた前進処理
    /// </summary>
    private void Move(float input)
    {
        // プレイヤーの「見た目の右方向」を求める
        float angularSpeed = Mathf.Abs(rb.angularVelocity.y);
        float moveForce = 1 + angularSpeed * speedFactor;
        Vector3 moveDir = transform.right * input;
        rb.AddForce(moveDir * moveForce, ForceMode.Force);
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    private void Jump()
    {
        rb.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
        isJumping = true;
    }

    /// <summary>
    ///着地判定
    /// </summary>

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

}