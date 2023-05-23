using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : Singleton<Menu>
{
    [Header("Weapon Shop")]
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponValueCanBuy;
    [SerializeField] private TextMeshProUGUI weaponValueCannotBuy;
    [SerializeField] private TextMeshProUGUI weaponBuffText;

    [SerializeField] private GameObject canBuyButton;
    [SerializeField] private GameObject cannotBuyButton;
    [SerializeField] private GameObject watchADButton;
    [SerializeField] private GameObject selectedButton;
    [SerializeField] private GameObject equipButton;

    [SerializeField] private WeaponScriptableObject weaponScriptableObject;

    [SerializeField] private Player player;

    [SerializeField] private LevelManager levelManager;

    public GameObject[] weapons;

    internal int currentWeaponIndex;

    private bool inWeaponShop = false;

    private void Start()
    {
        currentWeaponIndex = levelManager.currentWeaponIndex;
        foreach (var item in weapons)
        {
            item.SetActive(false);
        }
    }

    public void PlayClick()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
    }

    #region Weapon Shop

    public void SetActiveCurrentIndex()
    {
        weaponBuffText.SetText(weaponScriptableObject.GetBuffText(currentWeaponIndex));
        if (levelManager.openedWeaponIndex[currentWeaponIndex] == 0 && levelManager.openedWeaponIndex[currentWeaponIndex - 1] == 1)
        {
            canBuyButton.SetActive(true);
            watchADButton.SetActive(true);
            weaponValueCanBuy.SetText(weaponScriptableObject.GetWeaponValue(currentWeaponIndex).ToString());

            return;
        }

        if (levelManager.openedWeaponIndex[currentWeaponIndex] == 0 && levelManager.openedWeaponIndex[currentWeaponIndex - 1] == 0)
        {
            cannotBuyButton.SetActive(true);
            return;
        }

        if (levelManager.openedWeaponIndex[currentWeaponIndex] == 1)
        {
            if (player.currentPLayerWeaponIndex != currentWeaponIndex)
            {
                equipButton.SetActive(true);

            }
            else
            {
                selectedButton.SetActive(true);
            }
        }
    }

    public void SetDeactiveButton()
    {
        canBuyButton.SetActive(false);
        cannotBuyButton.SetActive(false);
        watchADButton.SetActive(false);
        selectedButton.SetActive(false);
        equipButton.SetActive(false);
    }

    public void WeaponClick()
    {
        inWeaponShop = true;
        currentWeaponIndex = player.currentPLayerWeaponIndex;
        weapons[currentWeaponIndex].SetActive(true);
        weaponName.SetText(weaponScriptableObject.GetWeaponName(currentWeaponIndex));
        SetDeactiveButton();
        SetActiveCurrentIndex();
    }

    public void ArrowRightClick()
    {
        weapons[currentWeaponIndex++].SetActive(false);
        currentWeaponIndex = Mathf.Min(currentWeaponIndex, weapons.Length - 1);
        weapons[currentWeaponIndex].SetActive(true);
        weaponName.SetText(weaponScriptableObject.GetWeaponName(currentWeaponIndex));
        SetDeactiveButton();
        SetActiveCurrentIndex();
    }

    public void ArrowLeftClick()
    {
        weapons[currentWeaponIndex--].SetActive(false);
        currentWeaponIndex = Mathf.Max(currentWeaponIndex, 0);
        weapons[currentWeaponIndex].SetActive(true);
        weaponName.SetText(weaponScriptableObject.GetWeaponName(currentWeaponIndex));
        SetDeactiveButton();
        SetActiveCurrentIndex();
    }

    public void BuyClick()
    {
        int subCoin = weaponScriptableObject.GetWeaponValue(currentWeaponIndex);
        if (levelManager.coin >= subCoin)
        {
            levelManager.coin -= subCoin;
            levelManager.SetCoinText();
            levelManager.openedWeaponIndex[currentWeaponIndex] = 1;
            SetDeactiveButton();
            SetActiveCurrentIndex();
        }
    }

    public void EquipClick()
    {
        player.weaponPrefab = weaponScriptableObject.GetWeaponPrefabs(currentWeaponIndex);
        player.currentPLayerWeaponIndex = currentWeaponIndex;
        equipButton.SetActive(false);
        selectedButton.SetActive(true);
    }

    public void XClick()
    {
        weapons[currentWeaponIndex].SetActive(false);
    }

    #endregion

    #region Skin Shop

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


    public void SetCurrentTab(int ButtonIndex)
    {
        for (int i = 0; i < scrollBars.Length; i++)
        {
            if (scrollBars[i].activeSelf==true)
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
        if(LevelManager.Instance.coin >= 500)
        {
            LevelManager.Instance.coin -= 500;
            LevelManager.Instance.SetCoinText();

            switch (currentTab)
            {
                case (int)SkinShopTab.Head:
                    LevelManager.Instance.openedHeadSkinIndex[currentButtonIndex] = 1;
                    SetCurrentTab(currentButtonIndex);
                    
                    if(headButtons[currentButtonIndex].LockIcon != null)
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
                levelManager.currentHeadSkinIndex = currentButtonIndex;
                headButtons[currentButtonIndex].EquipedIcon.SetActive(true);
                SetCurrentTab(currentButtonIndex);
                return;
            case (int)SkinShopTab.Pant:
                if (0 <= player.currentPlayerPantIndex && player.currentPlayerPantIndex < pantButtons.Count)
                    pantButtons[player.currentPlayerPantIndex].EquipedIcon.SetActive(false);
                player.currentPlayerPantIndex = currentButtonIndex;
                player.PantSkinClick(currentButtonIndex);
                levelManager.currentPantSkinIndex = currentButtonIndex;
                pantButtons[currentButtonIndex].EquipedIcon.SetActive(true);
                SetCurrentTab(currentButtonIndex);
                return;

            case (int)SkinShopTab.Armo:
                if (0 <= player.currentPlayerArmoIndex && player.currentPlayerArmoIndex < armoButtons.Count)
                    armoButtons[player.currentPlayerArmoIndex].EquipedIcon.SetActive(false);
                player.currentPlayerArmoIndex = currentButtonIndex;
                player.ArmoSkinClick(currentButtonIndex);
                levelManager.currentArmoSkinIndex = currentButtonIndex;
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
                levelManager.currentHeadSkinIndex = LevelManager.Instance.defaultHeadIndex;
                headButtons[currentButtonIndex].EquipedIcon.SetActive(false);
                SetCurrentTab(currentButtonIndex);
                return;
            case (int)SkinShopTab.Pant:
                player.currentPlayerPantIndex = LevelManager.Instance.defaultPantIndex;
                player.PantSkinClick(LevelManager.Instance.defaultPantIndex);
                levelManager.currentPantSkinIndex = LevelManager.Instance.defaultPantIndex;
                pantButtons[currentButtonIndex].EquipedIcon.SetActive(false);
                SetCurrentTab(currentButtonIndex);
                return;

            case (int)SkinShopTab.Armo:
                player.currentPlayerArmoIndex = LevelManager.Instance.defaultPantIndex;
                player.ArmoSkinClick(currentButtonIndex);
                levelManager.currentArmoSkinIndex = currentButtonIndex;
                armoButtons[currentButtonIndex].EquipedIcon.SetActive(false);
                SetCurrentTab(currentButtonIndex);
                return;

            default:

                return;
        }
    }

    #endregion
}
