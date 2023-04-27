using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BotIdleState : IState<Character>
{
    public float CountDownTime;
    public float WaitTime;

    public void OnEnter(Character t)
    {
        t.IsAttack = false;
        t.IsMoving = false;
        CountDownTime = Random.Range(1f, 2f);
        WaitTime = 0;
        t.ChangeAnim("Idle");
    }

    public void OnExecute(Character t)
    {
        WaitTime += Time.deltaTime;
        if(t.HaveTarget && WaitTime > 1f)
        {
            t.currentState.ChangeState(new BotAttackState());
            return;
        }

        CountDownTime -= Time.deltaTime;

        if(CountDownTime <= 0f)
        {
            t.currentState.ChangeState(new BotRunState());
            return;
        }
    }

    public void OnExit(Character t)
    {
        
    }
}
