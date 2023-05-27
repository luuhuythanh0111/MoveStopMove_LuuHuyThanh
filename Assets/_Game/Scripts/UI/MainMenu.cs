using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UICanvas
{
    public TextMeshProUGUI coinText;
    //public Button BtnPlay;
    private void Start()
    {
        //BtnPlay.onClick.AddListener(() => 
        //{ 
        //});
        LevelManager.Instance.coinText = coinText;
        LevelManager.Instance.SetCoinText();
    }

    public void SkinButton()
    {
        LevelManager.Instance.player.ChangeAnim("Dance_CharSkin");
        GameManager.Instance.cameraFollow.SetUpCameraForSkinShop();
        UIManager.Instance.OpenUI<SkinShop>();
        Close();
    }

    public void PlayButton()
    {
        UIManager.Instance.OpenUI<GamePlay>();
        Close();
    }

    public void WeaponButton()
    {
        UIManager.Instance.OpenUI<WeaponShop>();
        Close();
    }
}
