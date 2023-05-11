using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public Character character;
    public GameObject targetRing;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            character.currentState.ChangeState(new BotDieState());
        }
    }
}
