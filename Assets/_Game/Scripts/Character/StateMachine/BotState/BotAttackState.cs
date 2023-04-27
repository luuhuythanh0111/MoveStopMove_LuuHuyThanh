using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BotAttackState : IState<Character>
{
    public float CountDownResetAttackTime;
    public void OnEnter(Character t)
    {
        ((Bot)t).Attack();
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
            t.currentState.ChangeState(new BotIdleState());
        }

        if (t.CheckAnimationFinish())
        {
            t.ChangeAnim("Idle");
        }
    }

    public void OnExit(Character t)
    {
        t.IsAttack = false;
    }

}