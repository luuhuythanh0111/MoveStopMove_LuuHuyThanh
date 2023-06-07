using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public Character character;
    public GameObject targetRing;

    public void ActiveTargetRing()
    {
        targetRing.SetActive(true);
    }

    public void DeactiveTargetRing()
    {
        targetRing.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Cache.GetString("Weapon")))
        {
            character.currentState.ChangeState(new BotDieState());
            Weapon weapon = Cache.GetWeapon(other);
            if (character is Player)
            {
                Lose lose = UIManager.Instance.OpenUI<Lose>();
                lose.nameText.text = weapon.owner.characterName;
                UIManager.Instance.CloseUI<GamePlay>();
            }
        }
    }
}
