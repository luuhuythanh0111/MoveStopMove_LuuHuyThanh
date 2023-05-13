using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndex : MonoBehaviour
{
    public int ButtonIndexInSO;

    public Image LockIcon;
    public GameObject EquipedIcon;

    private void Start()
    {
        if (LockIcon == null)
            return;
        EquipedIcon.SetActive(false);

        if (LevelManager.Instance.openedWeaponIndex[ButtonIndexInSO]==0)
        {
            LockIcon.enabled = true;
        }
        else
        {
            LockIcon.enabled = false;

            if (LevelManager.Instance.currentHeadSkinIndex == ButtonIndexInSO)
                EquipedIcon.SetActive(true);
            else
            {
                
                EquipedIcon.SetActive(false);
            }
        }
    }
}
