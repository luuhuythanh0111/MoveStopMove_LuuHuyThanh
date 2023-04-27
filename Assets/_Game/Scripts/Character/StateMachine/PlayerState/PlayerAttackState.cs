using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerAttackState : IState<Character>
{
    public float CountDownResetAttackTime;
    public void OnEnter(Character t)
    {
        t.IsAttack = true;
        t.IsMoving = false;
        CountDownResetAttackTime = t.ResetAttackTime;
        t.ChangeAnim("Attack");
    }

    public void OnExecute(Character t)
    {
        CountDownResetAttackTime -= Time.deltaTime;
        if (CountDownResetAttackTime <= 0)
        {
            t.IsAttack = false;
            t.currentState.ChangeState(new PlayerIdleState());
        }

        if(t.CheckAnimationFinish())
        {
            t.ChangeAnim("Idle");
        }
    }

    public void OnExit(Character t)
    {
        
    }

}