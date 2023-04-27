using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class BotRunState : IState<Character>
{
    public float CountDownTime;
    public Transform targetEnemy;
    public void OnEnter(Character t)
    {
        t.IsAttack = false;
        t.IsMoving = true;
        CountDownTime = Random.Range(4f, 6f);
        ((Bot)t).SeekTarget(ref targetEnemy);
        ((Bot)t).SetDestination(targetEnemy.position);
        t.ChangeAnim("Run");
    }

    public void OnExecute(Character t)
    {
        CountDownTime -= Time.deltaTime;

        if (t.HaveTarget || CountDownTime <= 0f)
        {
            ((Bot)t).MoveStop();
            t.currentState.ChangeState(new BotIdleState());
        }
    }

    public void OnExit(Character t)
    {

    }
}
