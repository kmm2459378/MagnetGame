
public interface Player_state 
{ 
    Playerstate p_state { get; }
    void Entry();
    void Update();
    void Exit();

}

//ƒvƒŒƒCƒ„[‚Ìó‘Ô
public enum Playerstate 
{
    Idle,
    Move,
    Jump
}

