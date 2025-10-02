using UnityEngine;
[RequireComponent(typeof(Rigidbody))] public class North_Pole : MonoBehaviour
{ private Rigidbody rb; 
    private bool MagneticStop = true; 
    [SerializeField] private Transform target;
    [Header("N極の磁石")]
    [Tooltip("吸引力")]
    [SerializeField] private float suctionForce = 500f; 
    [Tooltip("吸引が始まる距離")]
    [SerializeField] private float maxDistance = 10f;
    [Tooltip("完全にくっつく距離")][SerializeField] 
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
    //色

    Gizmos.color = Color.cyan;
    //最大延の描画

    Gizmos.DrawWireSphere(transform.position, maxDistance); 
    //最小の範囲を描画
    Gizmos.color = Color.yellow;
     Gizmos.DrawWireSphere(transform.position, minDistance);
    }
    void FixedUpdate() 
    { 
        if (target == null || rb == null) return;
        //指定したものの位置を特定
        Vector3 direction = rb.position - target.position; 
        // 指定したものの距離
        float distance = direction.magnitude;
        Debug.DrawLine(target.position, rb.position, Color.red);
        if (distance < maxDistance && distance > minDistance && MagneticStop) {
            Debug.Log("接触した1"); 
            //ベクトルを1にする
            direction.Normalize();
            float strength = suctionForce ;
            Vector3 force = direction * strength;
            targetRb.AddForce(force, ForceMode.Force); 
            Debug.Log($"接触した1 距離:{distance:F2}, 力:{force.magnitude:F2}");
        } 
        else if (distance <= minDistance && MagneticStop) 
        { Debug.Log("接触した2"); 
            if (playerController != null && playerController.enabled)
            { 
                playerController.enabled = false; 
            } 
            MagneticStop = false;
        } 

        else if (distance > maxDistance)
        { targetRb.useGravity = true;
            // ← ここで必ず戻す
            if (playerController != null && !playerController.enabled)
            { 
                { Debug.Log("再有効化します"); 
                    
            playerController.enabled = true;
            Debug.Log($" playerController.enabled = {playerController.enabled}");
            Debug.Log($" targetRb.useGravity = {targetRb.useGravity}"); 
                } 
            } 
        } 
    }
}