using Unity.Netcode;
using UnityEngine;

namespace XRMultiplayer.MiniGames
{
    public class MiniGame_Escape : MiniGameBase
    {
        [SerializeField] private Transform redAnchor, blueAnchor;

        public override void Start()
        {
            base.Start();
        }

        public override void SetupGame()
        {
            base.SetupGame();
        }

        public override void StartGame()
        {
            base.StartGame();
            print("Game Starts!");
            ResetPlayerPosition();
        }

        public override void UpdateGame(float deltaTime)
        {
            base.UpdateGame(deltaTime);
        }

        public override void FinishGame(bool submitScore = true)
        {
            base.FinishGame(submitScore);
        }

        private void ResetPlayerPosition()
        {
            var players = NetworkManager.Singleton.ConnectedClientsList;

            if (players.Count >= 1 && redAnchor != null)
                Teleport(players[0].PlayerObject, blueAnchor);

            if (players.Count >= 2 && blueAnchor != null)
                Teleport(players[1].PlayerObject, redAnchor);

        }

        private void Teleport(NetworkObject playerObj, Transform anchor)
        {
            var t = playerObj.transform;

            t.position = anchor.position;
            t.rotation = anchor.rotation;
        }

    }
}

