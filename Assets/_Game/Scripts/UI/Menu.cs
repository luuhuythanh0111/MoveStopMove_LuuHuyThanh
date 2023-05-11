using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponValueCanBuy;
    [SerializeField] private TextMeshProUGUI weaponValueCannotBuy;

    [SerializeField] private GameObject canBuyButton;
    [SerializeField] private GameObject cannotBuyButton;
    [SerializeField] private GameObject watchADButton;
    [SerializeField] private GameObject selectedButton;
    [SerializeField] private GameObject equipButton;

    [SerializeField] private WeaponScriptableObject weaponScriptableObject;

    [SerializeField] private Player player;

    [SerializeField] private LevelManager levelManager;

    public GameObject[] weapons;


    private bool inWeaponShop = false;
    private int currentWeaponIndex;

    private void Start()
    {
        currentWeaponIndex = levelManager.currentWeaponIndex;
        foreach (var item in weapons)
        {
            item.SetActive(false);
        }
    }


    private void Update()
    {
        if(inWeaponShop)
        {
            return;
        }
    }

    public void SetActiveCurrentIndex()
    {
        if (levelManager.openedWeaponIndex[currentWeaponIndex] == 0 && (currentWeaponIndex == 0 || levelManager.openedWeaponIndex[currentWeaponIndex-1] == 1))
        {
            canBuyButton.SetActive(true);
            watchADButton.SetActive(true);
            weaponValueCanBuy.SetText(weaponScriptableObject.GetWeaponValue(currentWeaponIndex).ToString());

            return;
        }

        if (levelManager.openedWeaponIndex[currentWeaponIndex] == 1)
        {
            if(player.currentPLayerWeaponIndex != currentWeaponIndex)
            {
                equipButton.SetActive(true);
            
            }
            else
            {
                selectedButton.SetActive(true);
            }
        }

        if(levelManager.openedWeaponIndex[currentWeaponIndex] == 0 && levelManager.openedWeaponIndex[currentWeaponIndex - 1] == 0)
        {
            cannotBuyButton.SetActive(true);
        }
    }

    public void PlayClick()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
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
        levelManager.currentWeaponIndex = currentWeaponIndex;
        equipButton.SetActive(false);
        selectedButton.SetActive(true);
    }

    public void XClick()
    {
        weapons[currentWeaponIndex].SetActive(false);
    }
}
