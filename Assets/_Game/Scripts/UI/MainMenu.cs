using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UICanvas
{
    public TextMeshProUGUI coinText;
    public Button BtnMusicOn;
    public Button BtnMusicOff;

    public TMP_InputField inputField;

    private void Start()
    {
        LevelManager.Instance.coinText = coinText;
        LevelManager.Instance.SetCoinText();

        if(SoundManager.Instance.GetMusicAudioSourceMute() == true)
        {
            BtnMusicOff.gameObject.SetActive(true);
            BtnMusicOn.gameObject.SetActive(false);
        }
        else
        {
            BtnMusicOff.gameObject.SetActive(false);
            BtnMusicOn.gameObject.SetActive(true);
        }

        BtnMusicOff.onClick.AddListener(() => TurnSound());
        BtnMusicOn.onClick.AddListener(() => TurnSound());
    }

    public void SkinButton()
    {
        LevelManager.Instance.player.ChangeAnim("Dance_CharSkin");
        GameManager.Instance.cameraFollow.SetUpCameraForSkinShop();
        UIManager.Instance.OpenUI<SkinShop>();
        Close();
        SoundManager.Instance.PlayEffectSound((int)AudioClipEnum.ButtonClick);
    }

    public void PlayButton()
    {
        UIManager.Instance.OpenUI<GamePlay>();
        Close();
        SoundManager.Instance.PlayEffectSound((int)AudioClipEnum.ButtonClick);
    }

    public void WeaponButton()
    {
        UIManager.Instance.OpenUI<WeaponShop>();
        Close();
        SoundManager.Instance.PlayEffectSound((int)AudioClipEnum.ButtonClick);
    }

    public void TurnSound()
    {
        if (SoundManager.Instance.GetMusicAudioSourceMute() == true)
        {
            BtnMusicOff.gameObject.SetActive(false);
            BtnMusicOn.gameObject.SetActive(true);
            SoundManager.Instance.UnmuteMusicAudioSource();
        }
        else
        {
            BtnMusicOff.gameObject.SetActive(true);
            BtnMusicOn.gameObject.SetActive(false);
            SoundManager.Instance.MuteMusicAudioSource();
        }
    }

    public void ChangePLayerName()
    {
        LevelManager.Instance.player.SetName(inputField.text);
    }
}
