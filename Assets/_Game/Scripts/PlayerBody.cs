using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public Character character;
    public GameObject targetRing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            character.currentState.ChangeState(new BotDieState());
            Weapon weapon = Cache.GetWeapon(other);
            if (character is Player)
            {
                Lose lose = UIManager.Instance.OpenUI<Lose>();
                lose.nameText.text = weapon.character.characterName;
                UIManager.Instance.CloseUI<GamePlay>();
            }
        }
    }
}
