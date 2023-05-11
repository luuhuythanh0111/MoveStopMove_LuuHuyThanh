using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Bot botPrefab;
    [SerializeField] private TextMeshProUGUI coinText;

    internal int coin;
    internal int currentWeaponIndex;
    internal int[] openedWeaponIndex = new int[12];
    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin");
        currentWeaponIndex = PlayerPrefs.GetInt("currentWeaponIndex");

        for (int i = 0; i < openedWeaponIndex.Length; i++)
        {
            openedWeaponIndex[i] = PlayerPrefs.GetInt("openedWeaponIndex" + i.ToString());
        }

        coin = 1000;

        for (int i = 0; i < 10; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(botPrefab);
            bot.OnInit();
        }
    }

    public void SetCoinText()
    {
        coinText.SetText(coin.ToString());
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("currentWeaponIndex", currentWeaponIndex);
        for (int i = 0; i < openedWeaponIndex.Length; i++)
        {
            PlayerPrefs.SetInt("openedWeaponIndex" + i.ToString(), openedWeaponIndex[i]);
        }
    }

}
