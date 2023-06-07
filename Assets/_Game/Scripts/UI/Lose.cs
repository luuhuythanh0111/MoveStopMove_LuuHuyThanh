using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text nameText;

    public void ReviveButton()
    {
        LevelManager.Instance.player.Revive();
        UIManager.Instance.OpenUI<GamePlay>();
        Close();
    }

    public void HomeButton()
    {
        LevelManager.Instance.Reset();
        UIManager.Instance.OpenUI<MainMenu>();
        Close();
    }
}
