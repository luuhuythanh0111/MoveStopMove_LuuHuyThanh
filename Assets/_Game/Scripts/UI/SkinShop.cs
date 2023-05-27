using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShop : UICanvas
{
    [Header("Skin Shop")]
    [SerializeField] Button[] shopButton;
    [SerializeField] GameObject[] scrollBars;

    [Header("Head Skin")]
    [SerializeField] internal List<ButtonIndex> headButtons;

    [Header("Pant Skin")]
    [SerializeField] internal List<ButtonIndex> pantButtons;

    [Header("Armo Skin")]
    [SerializeField] internal List<ButtonIndex> armoButtons;

    [Header("Other")]
    [SerializeField] private GameObject canBuyButtonSkinShop;
    [SerializeField] private GameObject watchADButtonSkinShop;
    [SerializeField] private GameObject selectButtonSkinShop;
    [SerializeField] private GameObject unequipButtonSkinShop;
    [SerializeField] private TextMeshProUGUI skinBuffText;

    public Character player;

    private GameObject chooseFrameImagine;

    private enum SkinShopTab
    {
        Head,
        Pant,
        Armo,
        SetSkin,
    }

    private int currentTab;
    private int currentButtonIndex;

    private void Start()
    {
        player = LevelManager.Instance.player;
    }

    public void SetCurrentTab(int ButtonIndex)
    {
        for (int i = 0; i < scrollBars.Length; i++)
        {
            if (scrollBars[i].activeSelf == true)
            {
                currentTab = i;
                break;
            }
        }

        canBuyButtonSkinShop.SetActive(false);
        watchADButtonSkinShop.SetActive(false);
        selectButtonSkinShop.SetActive(false);
        unequipButtonSkinShop.SetActive(false);

        switch (currentTab)
        {
            case (int)SkinShopTab.Head:
                skinBuffText.SetText(LevelManager.Instance.skinScriptableObject.GetTextBuff((int)SkinShopTab.Head));
                if (LevelManager.Instance.openedHeadSkinIndex[ButtonIndex] == 0)
                {
                    canBuyButtonSkinShop.SetActive(true);
                    watchADButtonSkinShop.SetActive(true);
                }
                else if (LevelManager.Instance.openedHeadSkinIndex[ButtonIndex] == 1 && player.currentPLayerHeadIndex == ButtonIndex)
                {
                    unequipButtonSkinShop.SetActive(true);
                }
                else
                {
                    selectButtonSkinShop.SetActive(true);
                }
                return;
            case (int)SkinShopTab.Pant:
                skinBuffText.SetText(LevelManager.Instance.skinScriptableObject.GetTextBuff((int)SkinShopTab.Pant));
                if (LevelManager.Instance.openedPantSkinIndex[ButtonIndex] == 0)
                {
                    canBuyButtonSkinShop.SetActive(true);
                    watchADButtonSkinShop.SetActive(true);
                }
                else if (LevelManager.Instance.openedPantSkinIndex[ButtonIndex] == 1 && player.currentPlayerPantIndex == ButtonIndex)
                {
                    unequipButtonSkinShop.SetActive(true);
                }
                else
                {
                    selectButtonSkinShop.SetActive(true);
                }
                return;

            case (int)SkinShopTab.Armo:
                skinBuffText.SetText(LevelManager.Instance.skinScriptableObject.GetTextBuff((int)SkinShopTab.Armo));
                if (LevelManager.Instance.openedArmoSkinIndex[ButtonIndex] == 0)
                {
                    canBuyButtonSkinShop.SetActive(true);
                    watchADButtonSkinShop.SetActive(true);
                }
                else if (LevelManager.Instance.openedArmoSkinIndex[ButtonIndex] == 1 && player.currentPlayerArmoIndex == ButtonIndex)
                {
                    unequipButtonSkinShop.SetActive(true);
                }
                else
                {
                    selectButtonSkinShop.SetActive(true);
                }
                return;

            default:

                return;
        }

    }


    public void ResetAllButtonColor()
    {
        for (int i = 0; i < shopButton.Length; i++)
        {
            shopButton[i].image.color = new Color(0, 0, 0, 120f / 255);
        }

        for (int i = 0; i < scrollBars.Length; i++)
        {
            scrollBars[i].SetActive(false);
        }
    }

    public void SkinShopButtonClick(Button x)
    {
        x.image.color = new Color(0, 0, 0, 0);
    }

    public void SaveLastClickButton(GameObject clickButtonChooseFrame)
    {
        chooseFrameImagine = clickButtonChooseFrame;
    }

    public void ResetChooseFrame(int ButtonIndex)
    {
        if (chooseFrameImagine != null)
            chooseFrameImagine.SetActive(false);

        currentButtonIndex = ButtonIndex;
        SetCurrentTab(ButtonIndex);
    }

    public void ShopBuyClick()
    {
        if (LevelManager.Instance.coin >= 500)
        {
            LevelManager.Instance.coin -= 500;
            LevelManager.Instance.SetCoinText();

            switch (currentTab)
            {
                case (int)SkinShopTab.Head:
                    LevelManager.Instance.openedHeadSkinIndex[currentButtonIndex] = 1;
                    SetCurrentTab(currentButtonIndex);

                    if (headButtons[currentButtonIndex].LockIcon != null)
                        headButtons[currentButtonIndex].LockIcon.SetActive(false);
                    return;
                case (int)SkinShopTab.Pant:
                    LevelManager.Instance.openedPantSkinIndex[currentButtonIndex] = 1;
                    SetCurrentTab(currentButtonIndex);

                    pantButtons[currentButtonIndex].LockIcon.SetActive(false);
                    return;

                case (int)SkinShopTab.Armo:
                    LevelManager.Instance.openedArmoSkinIndex[currentButtonIndex] = 1;
                    SetCurrentTab(currentButtonIndex);

                    armoButtons[currentButtonIndex].LockIcon.SetActive(false);
                    return;

                default:

                    return;
            }
        }
    }

    public void SelectClick()
    {
        switch (currentTab)
        {
            case (int)SkinShopTab.Head:
                if (0 <= player.currentPLayerHeadIndex && player.currentPLayerHeadIndex < headButtons.Count)
                    headButtons[player.currentPLayerHeadIndex].EquipedIcon.SetActive(false);
                player.currentPLayerHeadIndex = currentButtonIndex;
                player.HeadSkinClick(currentButtonIndex);
                LevelManager.Instance.currentHeadSkinIndex = currentButtonIndex;
                headButtons[currentButtonIndex].EquipedIcon.SetActive(true);
                SetCurrentTab(currentButtonIndex);
                return;
            case (int)SkinShopTab.Pant:
                if (0 <= player.currentPlayerPantIndex && player.currentPlayerPantIndex < pantButtons.Count)
                    pantButtons[player.currentPlayerPantIndex].EquipedIcon.SetActive(false);
                player.currentPlayerPantIndex = currentButtonIndex;
                player.PantSkinClick(currentButtonIndex);
                LevelManager.Instance.currentPantSkinIndex = currentButtonIndex;
                pantButtons[currentButtonIndex].EquipedIcon.SetActive(true);
                SetCurrentTab(currentButtonIndex);
                return;

            case (int)SkinShopTab.Armo:
                if (0 <= player.currentPlayerArmoIndex && player.currentPlayerArmoIndex < armoButtons.Count)
                    armoButtons[player.currentPlayerArmoIndex].EquipedIcon.SetActive(false);
                player.currentPlayerArmoIndex = currentButtonIndex;
                player.ArmoSkinClick(currentButtonIndex);
                LevelManager.Instance.currentArmoSkinIndex = currentButtonIndex;
                armoButtons[currentButtonIndex].EquipedIcon.SetActive(true);
                SetCurrentTab(currentButtonIndex);
                return;

            default:

                return;
        }
    }

    public void Unequip()
    {
        switch (currentTab)
        {
            case (int)SkinShopTab.Head:
                player.currentPLayerHeadIndex = LevelManager.Instance.defaultHeadIndex;
                player.HeadSkinClick(LevelManager.Instance.defaultHeadIndex);
                LevelManager.Instance.currentHeadSkinIndex = LevelManager.Instance.defaultHeadIndex;
                headButtons[currentButtonIndex].EquipedIcon.SetActive(false);
                SetCurrentTab(currentButtonIndex);
                return;
            case (int)SkinShopTab.Pant:
                player.currentPlayerPantIndex = LevelManager.Instance.defaultPantIndex;
                player.PantSkinClick(LevelManager.Instance.defaultPantIndex);
                LevelManager.Instance.currentPantSkinIndex = LevelManager.Instance.defaultPantIndex;
                pantButtons[currentButtonIndex].EquipedIcon.SetActive(false);
                SetCurrentTab(currentButtonIndex);
                return;

            case (int)SkinShopTab.Armo:
                player.currentPlayerArmoIndex = LevelManager.Instance.defaultPantIndex;
                player.ArmoSkinClick(currentButtonIndex);
                LevelManager.Instance.currentArmoSkinIndex = currentButtonIndex;
                armoButtons[currentButtonIndex].EquipedIcon.SetActive(false);
                SetCurrentTab(currentButtonIndex);
                return;

            default:

                return;
        }
    }

    public void PopClick()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        Close();

        GameManager.Instance.cameraFollow.SetUpCameraForMainMenu();
        LevelManager.Instance.player.ChangeAnim("Idle");
    }
}
