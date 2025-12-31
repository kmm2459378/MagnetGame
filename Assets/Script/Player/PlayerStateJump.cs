using UnityEngine;

public class PlayerStateJump : Player_state
{
    Player p_player;
    public Playerstate p_state => Playerstate.Jump;
    public PlayerStateJump(Player player) => p_player = player;
    public void Entry() 
    {
        Debug.Log("ジャンプモードに移行");

        if (!p_player.isJumping)
        {
            p_player.DoJump();
        }
    }


    public void Update() 
    {
        if (!p_player.isJumping)
        {
            p_player.Idle();
        }
        //行動モード
        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D))) p_player.Move();

    }
    public void Exit() 
    {
        Debug.Log("ジャンプモード終了");
    }

}
