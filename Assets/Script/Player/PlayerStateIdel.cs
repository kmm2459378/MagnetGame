using UnityEngine;

public class PlayerStateIdel : Player_state
{
    Player p_player;
    
    public Playerstate p_state => Playerstate.Idle;
    public PlayerStateIdel(Player player) => p_player = player;
    public void Entry() 
    {
        Debug.Log("待機モードに入りました");
    }
    public void Update() 
    { 

      //行動モードに移行
      if(Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D)))
        {
            p_player.Move();
        }

      //ジャンプモードに移行
      else if (Input.GetKey(KeyCode.Space))
        {
            p_player.Jump();
        }
    }
    public void Exit() 
    {
        Debug.Log("待機モードを終了します");
    }
}
