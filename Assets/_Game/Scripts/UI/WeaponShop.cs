using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponShop : UICanvas
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

    [SerializeField] private Character player;

    public GameObject[] weapons;

    internal int currentWeaponIndex;

    private void Start()
    {
        player = LevelManager.Instance.player;
        currentWeaponIndex = LevelManager.Instance.currentWeaponIndex;
        foreach (var item in weapons)
        {
            item.SetActive(false);
        }

        currentWeaponIndex = player.currentPLayerWeaponIndex;
        weapons[currentWeaponIndex].SetActive(true);
        weaponName.SetText(weaponScriptableObject.GetWeaponName(currentWeaponIndex));
        SetDeactiveButton();
        SetActiveCurrentIndex();
    }
    public void SetActiveCurrentIndex()
    {
        weaponBuffText.SetText(weaponScriptableObject.GetBuffText(currentWeaponIndex));
        if (LevelManager.Instance.openedWeaponIndex[currentWeaponIndex] == 0 && LevelManager.Instance.openedWeaponIndex[currentWeaponIndex - 1] == 1)
        {
            canBuyButton.SetActive(true);
            watchADButton.SetActive(true);
            weaponValueCanBuy.SetText(weaponScriptableObject.GetWeaponValue(currentWeaponIndex).ToString());

            return;
        }

        if (LevelManager.Instance.openedWeaponIndex[currentWeaponIndex] == 0 && LevelManager.Instance.openedWeaponIndex[currentWeaponIndex - 1] == 0)
        {
            cannotBuyButton.SetActive(true);
            return;
        }

        if (LevelManager.Instance.openedWeaponIndex[currentWeaponIndex] == 1)
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
        if (LevelManager.Instance.coin >= subCoin)
        {
            LevelManager.Instance.coin -= subCoin;
            LevelManager.Instance.SetCoinText();
            LevelManager.Instance.openedWeaponIndex[currentWeaponIndex] = 1;
            SetDeactiveButton();
            SetActiveCurrentIndex();
        }
    }

    public void EquipClick()
    {
        player.weaponPrefab = weaponScriptableObject.GetWeaponPrefabs(currentWeaponIndex);
        player.currentPLayerWeaponIndex = currentWeaponIndex;
        player.WeaponClick(currentWeaponIndex);
        equipButton.SetActive(false);
        selectedButton.SetActive(true);
    }

    public void XClick()
    {
        weapons[currentWeaponIndex].SetActive(false);

        Close();
        UIManager.Instance.OpenUI<MainMenu>();
    }
}
