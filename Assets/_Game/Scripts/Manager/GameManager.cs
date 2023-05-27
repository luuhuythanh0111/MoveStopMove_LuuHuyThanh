using Unity.VisualScripting;
using UnityEngine;

public enum GameState { MainMenu, Gameplay, Pause }

public class GameManager : Singleton<GameManager>
{
    public CameraFollow cameraFollow;

    private GameState gameState;

    private void Start()
    {
        ChangeState(GameState.MainMenu);
        UIManager.Instance.GetUI<MainMenu>().Open();
    }

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }
}
