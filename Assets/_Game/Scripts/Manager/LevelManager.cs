using System;
using TMPro;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Bot botPrefab;
    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] internal SkinScriptableObject skinScriptableObject;

    internal int coin;

    internal int currentWeaponIndex;
    internal int[] openedWeaponIndex = new int[12];

    internal int currentHeadSkinIndex;
    internal int[] openedHeadSkinIndex = new int[10];

    internal int currentPantSkinIndex;
    internal int[] openedPantSkinIndex = new int[9];

    internal int currentArmoSkinIndex;
    internal int[] openedArmoSkinIndex = new int[2];

    /// <summary>
    /// Awake to take out the data of player
    /// </summary>
    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin");
        currentWeaponIndex = PlayerPrefs.GetInt("currentWeaponIndex");
        for (int i = 0; i < openedWeaponIndex.Length; i++)
        {
            openedWeaponIndex[i] = PlayerPrefs.GetInt("openedWeaponIndex" + i.ToString());
        }

        currentHeadSkinIndex = PlayerPrefs.GetInt("currentHeadSkinIndex");
        for (int i = 0; i < openedHeadSkinIndex.Length; i++)
        {
            openedHeadSkinIndex[i] = PlayerPrefs.GetInt("openedHeadSkinIndex" + i.ToString());
        }

        for (int i = 0; i < 10; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefab);
            bot.OnInit();
        }

        /// Reset info
        currentHeadSkinIndex = -1;
        currentWeaponIndex = 0;
        currentPantSkinIndex = 9;
        currentArmoSkinIndex = 2;
        for (int i = 0; i < openedWeaponIndex.Length; i++)
        {
            openedWeaponIndex[i] = 0;
        }
        openedWeaponIndex[currentWeaponIndex] = 1;
    }

    public void SetCoinText()
    {
        coinText.SetText(coin.ToString());
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("currentWeaponIndex", currentWeaponIndex); /// not this, make player set it weapon
        for (int i = 0; i < openedWeaponIndex.Length; i++)
        {
            PlayerPrefs.SetInt("openedWeaponIndex" + i.ToString(), openedWeaponIndex[i]);
        }

        for (int i = 0; i < openedHeadSkinIndex.Length; i++)
        {
            PlayerPrefs.SetInt("openedHeadSkinIndex" + i.ToString(), openedHeadSkinIndex[i]);
        }
    }

}
