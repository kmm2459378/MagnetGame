using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("カメラ追従")]
    [Tooltip("プレイヤーの位置")]
    [SerializeField] Transform player;

    [Tooltip("カメラの位置（オフセット）")]
    [SerializeField] Vector3 cameraPos = new Vector3(0,-1,-10);


    [Tooltip("カメラの固定Y軸")]
    [SerializeField] float fixedY = -1f;

    [Tooltip("カメラの固定Z軸")]
    [SerializeField] float fixedZ = -10f;


    void LateUpdate()
    {
        if (player == null) return;

        transform.position = new Vector3(
        player.position.x + cameraPos.x,
        fixedY,
        fixedZ
        );
            
    }
}
