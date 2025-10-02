using UnityEngine;
[RequireComponent(typeof(Rigidbody))] public class North_Pole : MonoBehaviour
{ private Rigidbody rb; 
    private bool MagneticStop = true; 
    [SerializeField] private Transform target;
    [Header("N�ɂ̎���")]
    [Tooltip("�z����")]
    [SerializeField] private float suctionForce = 500f; 
    [Tooltip("�z�����n�܂鋗��")]
    [SerializeField] private float maxDistance = 10f;
    [Tooltip("���S�ɂ���������")][SerializeField] 
    private float minDistance = 3f; 
    private Player_Move playerController;
    private Rigidbody targetRb; 
    private void Awake()
    { 
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false; 
        if (target != null) {
            targetRb = target.GetComponent<Rigidbody>(); 
            playerController = target.GetComponent<Player_Move>(); 
        } 
    }
    private void OnDrawGizmos()
    {
    //�F

    Gizmos.color = Color.cyan;
    //�ő剄�̕`��

    Gizmos.DrawWireSphere(transform.position, maxDistance); 
    //�ŏ��͈̔͂�`��
    Gizmos.color = Color.yellow;
     Gizmos.DrawWireSphere(transform.position, minDistance);
    }
    void FixedUpdate() 
    { 
        if (target == null || rb == null) return;
        //�w�肵�����̂̈ʒu�����
        Vector3 direction = rb.position - target.position; 
        // �w�肵�����̂̋���
        float distance = direction.magnitude;
        Debug.DrawLine(target.position, rb.position, Color.red);
        if (distance < maxDistance && distance > minDistance && MagneticStop) {
            Debug.Log("�ڐG����1"); 
            //�x�N�g����1�ɂ���
            direction.Normalize();
            float strength = suctionForce ;
            Vector3 force = direction * strength;
            targetRb.AddForce(force, ForceMode.Force); 
            Debug.Log($"�ڐG����1 ����:{distance:F2}, ��:{force.magnitude:F2}");
        } 
        else if (distance <= minDistance && MagneticStop) 
        { Debug.Log("�ڐG����2"); 
            if (playerController != null && playerController.enabled)
            { 
                playerController.enabled = false; 
            } 
            MagneticStop = false;
        } 

        else if (distance > maxDistance)
        { targetRb.useGravity = true;
            // �� �����ŕK���߂�
            if (playerController != null && !playerController.enabled)
            { 
                { Debug.Log("�ėL�������܂�"); 
                    
            playerController.enabled = true;
            Debug.Log($" playerController.enabled = {playerController.enabled}");
            Debug.Log($" targetRb.useGravity = {targetRb.useGravity}"); 
                } 
            } 
        } 
    }
}