using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlay : UICanvas
{
    public FloatingJoystick floatingJoystick;

    public TextMeshProUGUI AliveText;

    private void Start()
    {
        ((Player)LevelManager.Instance.player).floatingJoystick = floatingJoystick;
        LevelManager.Instance.aliveText = AliveText;
        LevelManager.Instance.SetAliveText();
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<Setting>();
        Close();
        GameManager.Instance.ChangeState(GameState.Pause);
    }
}
