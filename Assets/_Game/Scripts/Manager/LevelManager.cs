using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Bot botPrefab;
    [SerializeField] private Gift giftPrefab;

    [SerializeField] internal TextMeshProUGUI coinText;
    [SerializeField] internal TextMeshProUGUI aliveText;
    [SerializeField] internal SkinScriptableObject skinScriptableObject;
    [SerializeField] internal SkinIconScriptableObject skinIconScriptableObject;
    [SerializeField] internal NameScriptableObject nameScriptableObject;
    [SerializeField] internal ScaleScriptableObject scaleScriptableObject;
    [SerializeField] internal ColorScriptableObject colorScriptableObject;
    [SerializeField] internal Player player;
    
    internal int defaultWeaponIndex;
    internal int defaultHeadIndex;
    internal int defaultPantIndex;
    internal int defaultArmoIndex;

    internal int coin;
    internal int maxCharacterLevel;

    internal int currentWeaponIndex;
    internal int[] openedWeaponIndex = new int[12];

    internal int currentHeadSkinIndex;
    internal int[] openedHeadSkinIndex = new int[10];

    internal int currentPantSkinIndex;
    internal int[] openedPantSkinIndex = new int[9];

    internal int currentArmoSkinIndex;
    internal int[] openedArmoSkinIndex = new int[2];

    internal int aliveBot;
    internal int aliveCharacter = 30;

    internal int numberOfGift;

    const int maxAliveBot = 15;

    private void Start()
    {
        coin = PlayerPrefs.GetInt("coin");
        maxCharacterLevel = 0;
        numberOfGift = 0;
        currentWeaponIndex = PlayerPrefs.GetInt("currentWeaponIndex");
        currentHeadSkinIndex = PlayerPrefs.GetInt("currentHeadSkinIndex");
        currentPantSkinIndex = PlayerPrefs.GetInt("currentPantSkinIndex");
        currentArmoSkinIndex = PlayerPrefs.GetInt("currentArmoSkinIndex");

        for (int i = 0; i < openedWeaponIndex.Length; i++)
        {
            openedWeaponIndex[i] = PlayerPrefs.GetInt("openedWeaponIndex" + i.ToString());
        }
        
        for (int i = 0; i < openedHeadSkinIndex.Length; i++)
        {
            openedHeadSkinIndex[i] = PlayerPrefs.GetInt("openedHeadSkinIndex" + i.ToString());
        }
        for (int i = 0; i < openedPantSkinIndex.Length; i++)
        {
            openedPantSkinIndex[i] = PlayerPrefs.GetInt("openedPantSkinIndex" + i.ToString());
        }
        for (int i = 0; i < openedArmoSkinIndex.Length; i++)
        {
            openedArmoSkinIndex[i] = PlayerPrefs.GetInt("openedArmoSkinIndex" + i.ToString());
        }

        defaultWeaponIndex = 0;
        defaultArmoIndex = skinScriptableObject.armoSkin.Length;
        defaultHeadIndex = skinScriptableObject.headSkin.Length;
        defaultPantIndex = skinScriptableObject.pantSkin.Length;

        SpawnBot();

        /// Hack
        coin = 15000;

        openedWeaponIndex[currentWeaponIndex] = 1;

        /// Reset info
        //currentHeadSkinIndex = -1;
        //currentWeaponIndex = 0;
        //currentPantSkinIndex = 9;
        //currentArmoSkinIndex = 2;

        //for (int i = 0; i < openedWeaponIndex.Length; i++)
        //{
        //    openedWeaponIndex[i] = 0;
        //}
        //for (int i = 0; i < openedHeadSkinIndex.Length; i++)
        //{
        //    openedHeadSkinIndex[i] = 0;
        //}
        //for (int i = 0; i < openedPantSkinIndex.Length; i++)
        //{
        //    openedPantSkinIndex[i] = 0;
        //}
        //for (int i = 0; i < openedArmoSkinIndex.Length; i++)
        //{
        //    openedArmoSkinIndex[i] = 0;
        //}

    }

    public void SpawnBot()
    {
        for (int i = 0; i < 15; i++)
        {
            BotSpawn();
        }
    }

    public void SetAliveText()
    {
        if (aliveText == null)
            return;
        aliveText.SetText("Alive: " + aliveCharacter.ToString());
    }

    public void SetCoinText()
    {
        coinText.SetText(coin.ToString());
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("currentWeaponIndex", currentWeaponIndex); /// not this, make player set it weapon
        PlayerPrefs.SetInt("currentHeadSkinIndex", currentHeadSkinIndex);
        PlayerPrefs.SetInt("currentPantSkinIndex", currentPantSkinIndex);
        PlayerPrefs.SetInt("currentArmoSkinIndex", currentArmoSkinIndex);

        for (int i = 0; i < openedWeaponIndex.Length; i++)
        {
            PlayerPrefs.SetInt("openedWeaponIndex" + i.ToString(), openedWeaponIndex[i]);
        }

        for (int i = 0; i < openedHeadSkinIndex.Length; i++)
        {
            PlayerPrefs.SetInt("openedHeadSkinIndex" + i.ToString(), openedHeadSkinIndex[i]);
        }

        for (int i = 0; i < openedPantSkinIndex.Length; i++)
        {
            PlayerPrefs.SetInt("openedPantSkinIndex" + i.ToString(), openedPantSkinIndex[i]);
        }

        for (int i = 0; i < openedArmoSkinIndex.Length; i++)
        {
            PlayerPrefs.SetInt("openedArmoSkinIndex" + i.ToString(), openedArmoSkinIndex[i]);
        }
    }


    #region BotController

    List<Bot> listBots = new List<Bot>();

    public void BotSpawn()
    {
        Bot bot = SimplePool.Spawn<Bot>(botPrefab);
        bot.OnInit();
    }

    public void BotDespawn(Bot bot)
    {
        listBots.Remove(bot);
        aliveBot--;
        aliveCharacter--;
        SetAliveText();
        CheckGameState();
    }

    public void BotInit(Bot bot)
    {
        aliveBot++;
        SetAliveText();
        listBots.Add(bot);
    }

    #endregion

    private void CheckGameState()
    {
        if (aliveBot < 15 && aliveCharacter - aliveBot > 1)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefab);
            bot.OnInit();
        }

        if(aliveCharacter == 1)
        {

        }

        if (numberOfGift < 1)
        {
            GiftSpawn();
        }
    }

    List<Gift> listGifts = new List<Gift>();

    public void GiftSpawn()
    {
        Gift gift = SimplePool.Spawn<Gift>(giftPrefab);
        listGifts.Add(gift);
        numberOfGift++;
        gift.OnInit();
    }

    public void GiftDespawn(Gift gift)
    {
        listGifts.Remove(gift);
        numberOfGift--;
        CheckGameState();
    }

    public void Reset()
    {
        for(int i=0; i<listBots.Count; i++)
        {
            listBots[i].OnDespawn();
        }

        listBots.Clear();

        GameManager.Instance.ChangeState(GameState.MainMenu);
        aliveCharacter = 30;
        SpawnBot();
        player.OnInit();
        player.Revive();
    }
}
