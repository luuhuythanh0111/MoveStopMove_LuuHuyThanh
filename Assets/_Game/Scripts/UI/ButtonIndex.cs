using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndex : MonoBehaviour
{
    public int ButtonIndexInSO;

    public GameObject LockIcon;
    public GameObject EquipedIcon;

    public ButtonType buttonType;

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

}

public enum ButtonType
{
    HeadSkin,
    PantSkin,
    ArmoSkin,
}