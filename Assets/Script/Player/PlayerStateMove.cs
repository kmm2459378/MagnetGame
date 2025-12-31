using UnityEngine;

public class PlayerStateMove : Player_state 
{
   
    Player p_player;
    float rotationInput = 0;
    public float rotationTorque = 200;
    public float speedFactor = 1;

    public Playerstate p_state => Playerstate.Move;

    public PlayerStateMove(Player player) => p_player = player;

    public void Entry()
    {
        Debug.Log("動きモード");
    }
    public void Update() 
    {
        //待機モード
        if (Input.GetKey(KeyCode.Q))     p_player.Idle();
        //ジャンプモード
        if (Input.GetKey(KeyCode.Space)) p_player.Jump();
        
        //移動処理
        if (Input.GetKey(KeyCode.D))  rotationInput = -1f;
        // Aキー → 左回転{＆左方向へ加速
        else if (Input.GetKey(KeyCode.A))  rotationInput = 1f;
        else
        {
            p_player.Idle();
            return;
        }
            p_player.Rotate(rotationInput);
        p_player.RotateMove(rotationInput);

    }
    public void Exit()
    {
        Debug.Log("行動モード終了");
    }



}


