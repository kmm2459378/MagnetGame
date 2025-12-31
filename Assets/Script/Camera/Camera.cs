using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Camera : MonoBehaviour
{
    [Header("カメラ追従")]
    [Tooltip("カメラの基本設定")]
    [SerializeField] private CameraPresent present = CameraPresent.Normal;

    [Tooltip("プレイヤーの位置")]
    [SerializeField] Transform p_target;

    [Tooltip("カメラの位置（オフセット）")]
    [SerializeField] Vector3 cameraPos = new Vector3(0,-1,-10);

    private CameraFollowBehavior c_Behaviour;

    private float lastx;
    private float lookAheadPos;
    private float lookAheadVelocity;

    void Start()
    {
        c_Behaviour = CameraBehaviorFactory.Create(present);
        lastx = p_target.position.x;
    }

    void LateUpdate()
    {
        if (p_target == null || c_Behaviour == null) return;

        //追跡
        float moveX = p_target.position.x - lastx;
        lookAheadPos = Mathf.SmoothDamp(
            lookAheadPos,
            Mathf.Sign(moveX) * c_Behaviour.LookAheadDistance,
            ref lookAheadVelocity,
            c_Behaviour.LookAheadSmooth
        );

        lastx = p_target.position.x;

        //目標座標 
        Vector3 targetPos = p_target.position + cameraPos;
        targetPos.x += lookAheadPos;

        float newX = Mathf.Lerp(transform.position.x, targetPos.x, Time.deltaTime / c_Behaviour.SmoothX);
        float newY = Mathf.Lerp(transform.position.y, targetPos.y, Time.deltaTime / c_Behaviour.SmoothY);

        transform.position = new Vector3(newX, newY, targetPos.z);
    }
}
