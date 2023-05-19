using UnityEngine;

public class StateMachine<T> where T : Character
{
    private IState<T> currentState;
    private T typeClass;

    public void ChangeState<TState>(TState state) where TState : IState<T>
    {
        if(GameManager.Instance.IsState(GameState.MainMenu))
        {
            if(currentState == null)
            {
                currentState = state;
                if (currentState != null)
                {
                    currentState.OnEnter(typeClass);
                }
            }
            return;
        }
        //Debug.Log(currentState + " -> "   + state);
        if (currentState != null)
        {
            currentState.OnExit(typeClass);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(typeClass);
        }
    }

    public void UpdateState(T owner)
    {
        if (currentState != null)
        {
            currentState.OnExecute(owner);
        }
    }

    public void SetOwner(T owner)
    {
        typeClass = owner;
    }
}