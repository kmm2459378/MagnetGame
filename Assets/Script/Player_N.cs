using UnityEngine;

public class Player_N : MonoBehaviour
{

    [SerializeField] private string targetTagNorth = "North";
    [SerializeField] private string targetTagSouth = "South";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTagNorth))
        {
            Debug.Log("Player �� Magnet �ɐڐG����");

            // ���肪 North_Pole �������Ă�����
            North_Pole magnet = other.GetComponent<North_Pole>();
            if (magnet != null)
            {
                // ����J�n
                magnet.enabled = true;

                // �K�v�Ȃ狭���I�� Awake �����̏������Ă�
                magnet.InitializeMagnet();
            }
        }

        else if (other.CompareTag(targetTagSouth))
        {
            South_Gimick magnet = other.GetComponent<South_Gimick>();
            //��������S�ɂ���܂�
        }
    }

}
