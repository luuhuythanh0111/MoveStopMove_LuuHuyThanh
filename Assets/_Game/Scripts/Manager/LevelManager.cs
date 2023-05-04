using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Bot botPrefab;

    private void Awake()
    {

        //for (int i = 0; i < 3; i++)
        //{
        //    Bot bot = SimplePool.Spawn<Bot>(botPrefab);
        //    bot.OnInit();
        //}
    }
}
