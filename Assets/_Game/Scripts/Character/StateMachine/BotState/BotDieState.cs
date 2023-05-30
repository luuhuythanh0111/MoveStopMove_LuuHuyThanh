using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDieState : IState<Character>
{
    public float timer;
    public float dieTime;
    
    private bool haveDespawn;

    public void OnEnter(Character t)
    {
        t.IsAttack = false;
        t.IsMoving = false;
        dieTime = 2.1f;
        timer = 0;
        t.ChangeAnim("Dead");
        t.playerBody.gameObject.layer = 0;
        haveDespawn = false;
        EffectManager.Instance.PlayBloodParticle(t.transform);
        

        if (t.wayPointMarker != null)
        {
            t.wayPointMarker.OnDespawn();
        }

        if(t is Player)
        {
            ((Player)t).rigidbody.velocity = Vector3.zero;
        }

    }

    public void OnExecute(Character t)
    {
        if (haveDespawn)
            return;
        timer += Time.deltaTime;

        if(timer >= dieTime)
        {
            haveDespawn = true;
            t.OnDespawn();
            EffectManager.Instance.PlayDeathParticle(t.transform);
        }
        t.isDead = true;
    }

    public void OnExit(Character t)
    {
        t.playerBody.gameObject.layer = 3;
    }
}
