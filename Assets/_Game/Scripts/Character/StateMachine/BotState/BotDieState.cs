using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDieState : IState<Character>
{
    public float timer;
    public float dieTime;
    public void OnEnter(Character t)
    {
        t.IsAttack = false;
        t.IsMoving = false;
        dieTime = 2.1f;
        timer = 0;
        t.ChangeAnim("Dead");
        t.playerBody.gameObject.layer = 0;
    }

    public void OnExecute(Character t)
    {
        timer += Time.deltaTime;

        if(timer >= dieTime)
        {
            t.OnDespawn();
            SimplePool.Despawn(t);
        }
        t.isDead = true;
    }

    public void OnExit(Character t)
    {
        t.playerBody.gameObject.layer = 3;
    }
}
