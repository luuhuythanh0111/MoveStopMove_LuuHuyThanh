using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerRunState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.IsMoving = true;
        
    }

    public void OnExecute(Character t)
    {
        t.ChangeAnim("Run");
        
        ((Player)t).Move();
    }

    public void OnExit(Character t)
    {

    }
}