using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerIdleState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.IsAttack = false;
        t.IsMoving = false;
        t.ChangeAnim("Idle");

    }

    public void OnExecute(Character t)
    {
        if(t.HaveTarget)
        {
            ((Player)t).Attack();
        }
    }

    public void OnExit(Character t)
    {
        
    }

}