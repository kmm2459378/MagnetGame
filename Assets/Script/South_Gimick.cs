using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class South_Gimick : MonoBehaviour
{
    private Rigidbody rb;
    private bool MagneticStop = true;
    [SerializeField] private Transform target;
    [Header("Nã…ÇÃé•êŒ")]
    [SerializeField] private float suctionForce = 500f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float minDistance = 3f;

    private Player_Move playerController;
    private Rigidbody targetRb;

    private void Awake()
    {
        // ç≈èâÇÕé~ÇﬂÇƒÇ®Ç´ÇΩÇ¢Ç»ÇÁÇ±Ç±Ç≈ñ≥å¯âª
        this.enabled = false;
    }

    // äOÇ©ÇÁåƒÇÒÇ≈èâä˙âªÇ≈Ç´ÇÈÇÊÇ§Ç…Ç∑ÇÈ
    public void InitializeMagnet()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;

        if (target != null)
        {
            targetRb = target.GetComponent<Rigidbody>();
            playerController = target.GetComponent<Player_Move>();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, maxDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }

    private void FixedUpdate()
    {
        if (target == null || rb == null) return;

        Vector3 direction = rb.position - target.position;
        float distance = direction.magnitude;

        Debug.DrawLine(target.position, rb.position, Color.red);

        if (distance < maxDistance && distance > minDistance && MagneticStop)
        {
            direction.Normalize();
            Vector3 force = direction * suctionForce;
            targetRb.AddForce(force, ForceMode.Force);
        }
        else if (distance <= minDistance && MagneticStop)
        {
            if (playerController != null && playerController.enabled)
            {
                playerController.enabled = false;
            }
            MagneticStop = false;
        }
        else if (distance > maxDistance)
        {
            targetRb.useGravity = true;
            if (playerController != null && !playerController.enabled)
            {
                playerController.enabled = true;
            }
        }
    }
}
