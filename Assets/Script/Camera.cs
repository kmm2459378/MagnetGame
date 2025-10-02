using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("�J�����Ǐ]")]
    [Tooltip("�v���C���[�̈ʒu")]
    [SerializeField] Transform player;

    [Tooltip("�J�����̈ʒu�i�I�t�Z�b�g�j")]
    [SerializeField] Vector3 cameraPos = new Vector3(0,-1,-10);


    [Tooltip("�J�����̌Œ�Y��")]
    [SerializeField] float fixedY = -1f;

    [Tooltip("�J�����̌Œ�Z��")]
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
