using System.Collections.Generic;
using UnityEngine;

public class PlayerContext
{
    Player_state p_currentState;
    Player_state p_previousState;

    Dictionary<Playerstate, Player_state> p_stateTable;

    public void Init(Player player, Playerstate initstate)
    {
        if (p_stateTable != null) return;

        Dictionary<Playerstate, Player_state> p_table  = new()
        {
            {Playerstate.Idle, new PlayerStateIdel(player)},
            {Playerstate.Move, new PlayerStateMove(player)},
            {Playerstate.Jump, new PlayerStateJump(player)},
        };
        p_stateTable = p_table;
        ChangeState(initstate);
    }

    public void ChangeState(Playerstate next)
    {
        if (p_stateTable == null) return;
        if (p_currentState != null && p_currentState.p_state == next)
        {
            return; 
        }

        var p_nextState = p_stateTable[next];
        p_previousState = p_currentState;
        p_previousState?.Exit();
        p_currentState = p_nextState;
        p_currentState.Entry();
    }
    public void Update() => p_currentState?.Update();
} 

  