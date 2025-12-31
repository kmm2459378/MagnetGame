using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMagnetTarget : MonoBehaviour
{
    public Rigidbody p_rb  { get; private set; }
    public Player p_control {  get; private set; }

    private void Awake()
    {
        p_rb = GetComponent<Rigidbody>();
        p_control = GetComponent<Player>();
    }
    

    public void DisableControl()
    {
        if(p_control != null)
        {

        }
    }

    public void EnableControl()
    {

    }
}
