using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class IdleState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.IsMoving = false;
        t.ChangeAnim("Idle");
    }

    public void OnExecute(Character t)
    {
        t.Attack();
    }

    public void OnExit(Character t)
    {
        
    }

}