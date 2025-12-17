using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace XRMultiplayer.MiniGames
{
    public enum DoorType
    {
        FirstGame,
        SecondGame,
        Exit
    }

    public class MiniGame_Escape : MiniGameBase
    {
        public ulong initiatorClientId; //the player who initiated the mini game

        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject teleportAnchorsParent;
        [SerializeField] private GameObject middleCorridorSeparator;
        [SerializeField] private Transform redAnchor, blueAnchor;
        [SerializeField] private Transform[] doors;

        private MiniGameManager miniGameManager;
        private bool isPlayerOne = false;
        private ulong selfId = 0;
        
        public override void Start()
        {
            base.Start();
            miniGameManager = GetComponent<MiniGameManager>();
            selfId = NetworkManager.Singleton.LocalClientId;
        }

        public override void SetupGame()
        {
            base.SetupGame();

            teleportAnchorsParent.SetActive(true);
            //middleCorridorSeparator.SetActive(false);

            foreach (var d in doors)
            {
                d.gameObject.SetActive(true);
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            print("Game Starts!");
            //SetupPlayersPosition();

            OpenDoors(DoorType.FirstGame);
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

        public void FirstSubMiniGameCompleteTrigger()
        {
            Debug.Log("First Sub MiniGame Complete Triggered");
            OpenDoors(DoorType.SecondGame);
        }

        public void SecondSubMiniGameCompleteTrigger()
        {
            Debug.Log("Second Sub MiniGame Complete Triggered");
            OpenDoors(DoorType.Exit);
        }

        private void Teleport(NetworkObject playerObj, Transform anchor)
        {
            if(anchor == null)
                return;
            Debug.LogWarning($"Teleporting player {playerObj.OwnerClientId} to anchor.");
            var playerController = playerObj.GetComponent<CharacterController>();
            if (playerController != null) playerController.enabled = false;

            playerObj.transform.SetPositionAndRotation(anchor.position, anchor.rotation);

            if (playerController != null) playerController.enabled = true;
        }

        private Transform GetClosestAnchor(Transform player)
        {
            float distRed = Vector3.Distance(player.position, redAnchor.position);
            float distBlue = Vector3.Distance(player.position, blueAnchor.position);

            return (distRed < distBlue) ? redAnchor : blueAnchor;
        }

        private void OpenDoors(DoorType type)
        {
            switch (type)
            {
                case DoorType.FirstGame:
                    if (isPlayerOne)
                        doors[0].gameObject.SetActive(false);
                    else
                        doors[2].gameObject.SetActive(false);
                    break;
                case DoorType.SecondGame:
                    if (isPlayerOne)
                        doors[1].gameObject.SetActive(false);
                    else
                        doors[3].gameObject.SetActive(false);
                    break;
                case DoorType.Exit:
                    doors[4].gameObject.SetActive(false);
                    break;
            }
        }

        private void SetupPlayersPosition()
        {
            var players = miniGameManager.GetCurrentPlayers();

            if (players.Count < 2)
            {
                Debug.LogError("Not enough players to setup the player's position");
                return;
            }

            ulong initiatorId = players[0];
            ulong otherId = players[1];

            Debug.LogWarning($"Player 0 ID : {initiatorClientId}. is self? {initiatorClientId == selfId}");
            Debug.LogWarning($"Player 1 ID : {otherId}. is self? {initiatorClientId == selfId}");

            NetworkObject initiator = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(initiatorId);
            NetworkObject other = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(otherId);

            Transform anchorForInitiator = GetClosestAnchor(initiator.transform);
            Transform anchorForOther = (anchorForInitiator == redAnchor) ? blueAnchor : redAnchor;

            Teleport(initiator, anchorForInitiator);
            Teleport(other, anchorForOther);
        }
    }
}

