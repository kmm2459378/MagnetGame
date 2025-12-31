using UnityEngine;

public class Player : MonoBehaviour
{
    

    [Header("プレイヤー設定")]
    [Tooltip("回転させる力の大きさ")]
    [SerializeField] private float rotationTorque = 20000f;

    [Tooltip("回転に応じた速度上昇率")]
    [SerializeField] private float speedFactor = 1f;

    [Tooltip("ジャンプ力")]
    [SerializeField] private float Jumpforce = 100f;

    [Tooltip("摩擦力（通常）")]
    [SerializeField] public PhysicsMaterial normalMat;

    [Tooltip("摩擦力（移動時）")]
    [SerializeField] public PhysicsMaterial MovingMat;

    [SerializeField] private string targetTagNorth = "North";
    [SerializeField] private string targetTagSouth = "South";

    private Collider col;
    private Rigidbody rb;

    PlayerContext p_context;
    public bool isJumping = false;

    private void Awake()
    {
        p_context = new PlayerContext();
        p_context.Init(this, Playerstate.Idle);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        //回転はY軸のみ制限
        rb.constraints = RigidbodyConstraints.FreezePositionZ |
        RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationZ;

    }

    private void Update() => p_context.Update();

    public void Idle() => p_context.ChangeState(Playerstate.Idle);
    public void Move() => p_context.ChangeState(Playerstate.Move);
    public void Jump() => p_context.ChangeState(Playerstate.Jump);

    /// <summary>
    /// 回転処理
    /// </summary>
    public void Rotate(float input)
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
    public void RotateMove(float input)
    {
        // プレイヤーの「見た目の右方向」を求める
        float angularSpeed = Mathf.Abs(rb.angularVelocity.y);
        float moveForce = 1 + angularSpeed * speedFactor;
        Vector3 moveDir = transform.right * input;
        rb.AddForce(moveDir * moveForce, ForceMode.Force);
    }

    /// <summary>
    /// ジャンプ実行
    /// </summary>
    public void DoJump()
    {
        rb.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
        isJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

}
