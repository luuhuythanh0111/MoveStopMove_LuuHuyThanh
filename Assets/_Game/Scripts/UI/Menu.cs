using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : Singleton<Menu>
{

    public void PlayClick()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
    }

    
}
