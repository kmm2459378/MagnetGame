using UnityEngine;

public class Player_N : MonoBehaviour
{

    [SerializeField] private string targetTagNorth = "North";
    [SerializeField] private string targetTagSouth = "South";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTagNorth))
        {
            Debug.Log("Player が Magnet に接触した");

            // 相手が North_Pole を持っていたら
            North_Pole magnet = other.GetComponent<North_Pole>();
            if (magnet != null)
            {
                // 動作開始
                magnet.enabled = true;

                // 必要なら強制的に Awake 相当の処理を呼ぶ
                magnet.InitializeMagnet();
            }
        }

        else if (other.CompareTag(targetTagSouth))
        {
            South_Gimick magnet = other.GetComponent<South_Gimick>();
            //ここからS極つくります
        }
    }

}
