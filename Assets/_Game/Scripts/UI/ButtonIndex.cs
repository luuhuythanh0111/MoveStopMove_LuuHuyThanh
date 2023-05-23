using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndex : GameUnit
{
    public int ButtonIndexInSO;

    public GameObject ChooseFrame;
    public GameObject LockIcon;
    public GameObject EquipedIcon;
    public Image BackGround;

    public ButtonType buttonType;
    public Button button;

    private void Start()
    {
        if (LockIcon == null)
            return;
        EquipedIcon.SetActive(false);

        switch(buttonType)
        {
            case ButtonType.HeadSkin:
                if (LevelManager.Instance.openedHeadSkinIndex[ButtonIndexInSO] == 0)
                {
                    LockIcon.SetActive(true);
                }
                else
                {
                    LockIcon.SetActive(false);

                    if (LevelManager.Instance.currentHeadSkinIndex == ButtonIndexInSO)
                        EquipedIcon.SetActive(true);
                    else
                    {

                        EquipedIcon.SetActive(false);
                    }
                }
                return;
            case ButtonType.PantSkin:
                if (LevelManager.Instance.openedPantSkinIndex[ButtonIndexInSO] == 0)
                {
                    LockIcon.SetActive(true);
                }
                else
                {
                    LockIcon.SetActive(false);

                    if (LevelManager.Instance.currentPantSkinIndex == ButtonIndexInSO)
                        EquipedIcon.SetActive(true);
                    else
                    {

                        EquipedIcon.SetActive(false);
                    }
                }
                return;
            case ButtonType.ArmoSkin:
                if (LevelManager.Instance.openedArmoSkinIndex[ButtonIndexInSO] == 0)
                {
                    LockIcon.SetActive(true);
                }
                else
                {
                    LockIcon.SetActive(false);
                    if (LevelManager.Instance.currentArmoSkinIndex == ButtonIndexInSO)
                        EquipedIcon.SetActive(true);
                    else
                    {
                        EquipedIcon.SetActive(false);
                    }
                }
                return;
            default:
                return;
        }
        
    }

    public void Click()
    {
        Menu.Instance.ResetChooseFrame(ButtonIndexInSO);
        ChooseFrame.SetActive(true);
        Menu.Instance.SaveLastClickButton(ChooseFrame);
        switch (buttonType)
        {
            case ButtonType.HeadSkin:
                LevelManager.Instance.player.HeadSkinClick(ButtonIndexInSO);
                return;
            case ButtonType.PantSkin:
                LevelManager.Instance.player.PantSkinClick(ButtonIndexInSO);
                return;
            case ButtonType.ArmoSkin:
                LevelManager.Instance.player.ArmoSkinClick(ButtonIndexInSO);
                return;
            default:
                return;
        }
        
    }

    public override void OnInit()
    {
        
    }

    public override void OnInit(Character t)
    {
        throw new System.NotImplementedException();
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDespawn()
    {
        throw new System.NotImplementedException();
    }
}

public enum ButtonType
{
    HeadSkin,
    PantSkin,
    ArmoSkin,
}