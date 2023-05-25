using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void SkinButton()
    {
        UIManager.Instance.GetUI<SkinShop>().Open();
        Close();
    }
}
