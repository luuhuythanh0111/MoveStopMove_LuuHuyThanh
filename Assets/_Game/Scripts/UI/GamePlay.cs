using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : UICanvas
{
    public FloatingJoystick floatingJoystick;

    private void Start()
    {
        ((Player)LevelManager.Instance.player).floatingJoystick = floatingJoystick;
    }
}
