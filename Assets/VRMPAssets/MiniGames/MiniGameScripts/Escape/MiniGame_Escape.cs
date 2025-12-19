using System;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

namespace XRMultiplayer.MiniGames
{
    [Serializable]
    public enum EscapeGameState
    {
        None,
        FirstGame,
        SecondGame,
        Exit
    }

    public class MiniGame_Escape : MiniGameBase
    {
        public bool isPlayerInRedZone { get; private set;}

        [HideInInspector] public EscapeGameState CurrentEscapeGameState { get; private set; }
        public event Action<EscapeGameState> OnStateEntered;
        public event Action<EscapeGameState> OnStateExited;

        private CubeSubMiniGame cubeSubMiniGame;
        private MiniGameManager MGM;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject teleportAnchorsParent;
        [SerializeField] private GameObject middleCorridorSeparator;
        [SerializeField] private Transform redAnchor, blueAnchor;
        [SerializeField] private Transform[] doors;

        private void Awake()
        {
            cubeSubMiniGame = GetComponent<CubeSubMiniGame>();
            MGM = GetComponent<MiniGameManager>();
        }

        public override void Start()
        {
            base.Start();
            ChangeState(EscapeGameState.None);
        }

        public override void SetupGame()
        {
            base.SetupGame();
            
            teleportAnchorsParent.SetActive(true);

            foreach (var d in doors)
            {
                d.gameObject.SetActive(true);
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            print("Game Starts!");
            isPlayerInRedZone = IsPlayerInRedZone();
            ChangeState(EscapeGameState.FirstGame);
        }

        public override void UpdateGame(float deltaTime)
        {
            base.UpdateGame(deltaTime);
            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            timerText.text = TimeSpan.FromSeconds(Mathf.CeilToInt(m_CurrentTimer)).ToString(@"mm\:ss");
        }

        public override void FinishGame(bool submitScore = true)
        {
            base.FinishGame(submitScore);
        }

        private bool IsPlayerInRedZone()
        {
            XRINetworkPlayer localPlayer = XRINetworkPlayer.LocalPlayer;

            if (localPlayer == null)
            {
                Debug.LogError("Local player not found.");
                return false;
            }

            // Position RELATIVE au mini-jeu
            Vector3 localPos = transform.InverseTransformPoint(localPlayer.transform.position);
            Debug.LogWarning($"Local Player Position: {localPos}");
            return localPos.z > 0f;
        }

        #region State Machine
        public void ChangeState(EscapeGameState newState)
        {
            if (newState == CurrentEscapeGameState)
                return;

            ExitState(CurrentEscapeGameState);
            CurrentEscapeGameState = newState;
            EnterState(CurrentEscapeGameState);
        }

        private void EnterState(EscapeGameState state)
        {
            Debug.Log($"Enter state: {state}");
            OnStateEntered?.Invoke(state);

            switch (state)
            {
                case EscapeGameState.FirstGame:
                    doors[0].gameObject.SetActive(false);
                    doors[2].gameObject.SetActive(false);
                    cubeSubMiniGame.Init(isPlayerInRedZone);
                    break;

                case EscapeGameState.SecondGame:
                    doors[1].gameObject.SetActive(false);
                    doors[3].gameObject.SetActive(false);
                    break;

                case EscapeGameState.Exit:
                    doors[4].gameObject.SetActive(false);
                    doors[5].gameObject.SetActive(false);
                    break;
            }
        }

        private void ExitState(EscapeGameState state)
        {
            Debug.Log($"Exit state: {state}");
            OnStateExited?.Invoke(state);

            switch (state)
            {
                case EscapeGameState.FirstGame:
                    // Cleanup premier jeu
                    break;

                case EscapeGameState.SecondGame:
                    // Cleanup second jeu
                    break;
            }
        }
        #endregion
    }
}

