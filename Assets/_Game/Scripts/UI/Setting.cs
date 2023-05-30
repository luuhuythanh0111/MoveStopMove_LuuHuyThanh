using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public void ContinueButton()
    {
        UIManager.Instance.OpenUI<GamePlay>();
        Close();
        GameManager.Instance.ChangeState(GameState.Gameplay);
    }
}
