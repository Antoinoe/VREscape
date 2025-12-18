using UnityEngine;
using XRMultiplayer.MiniGames;

public class DebugEscapeMiniGame : MonoBehaviour
{
    [field : SerializeField] public EscapeGameState gameState { get; private set; } 

    MiniGame_Escape mg;
    MiniGameManager mgManager;

    private void Awake()
    {
        mg = GetComponent<MiniGame_Escape>();
        mgManager = GetComponent<MiniGameManager>();
    }

    public void ChangeGameState(EscapeGameState state)
    {
        mg.ChangeState(state);
    }

    public void StartGame()
    {
        mgManager.EnableStartTrigger();
    }
}
