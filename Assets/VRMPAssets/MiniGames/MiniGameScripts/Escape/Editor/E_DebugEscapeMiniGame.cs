using UnityEngine;
using UnityEditor;
using System.Collections;
using XRMultiplayer.MiniGames;

namespace XRMultiplayer
{
    [CustomEditor(typeof(DebugEscapeMiniGame))]
    public class E_DebugEscapeMiniGame : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DebugEscapeMiniGame t = (DebugEscapeMiniGame)target;

            if (GUILayout.Button("Change Game State"))
            {
                t.ChangeGameState(t.gameState);
            }

            if(GUILayout.Button("Start Game"))
            {
                t.StartGame();
            }
        }
    }
}

