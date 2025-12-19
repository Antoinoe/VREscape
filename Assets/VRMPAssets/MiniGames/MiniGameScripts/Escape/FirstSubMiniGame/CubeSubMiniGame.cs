using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;
using System;

namespace XRMultiplayer.MiniGames
{
    public enum EPattern
    {
        A1,
        BORROMEAN,
        CIRTRIANGLE,
        ENNEAGRAM,
        FLEURDEVIE,
        HEPTAGRAM,
        MAZE,
        OCTAGRAM,
        TRIFORCE,
        GRID,
        SACRED,
        SIERPINSKI,
        SPIRAL,
        YINYANG
    }

    public class CubeSubMiniGame : NetworkBehaviour
    {
        public NetworkList<int> solutionsId;

        private const string TEXTURE_PATH = "Assets/VRMPAssets/MiniGames/MiniGameTextures/Escape";
        private const int NB_SOLUTIONS = 8;
        private const int NB_MAX_CUBES = 20;

        [SerializeField] private GameObject cubePrefab;
        [SerializeField] private Dictionary<EPattern, Texture2D> patternTextures;
        //Red desk ref
        //Blue desk ref

        private List<GameObject> dynamicCubes = new List<GameObject>();
        private List<EPattern> solutions = new List<EPattern>();

        private void Awake()
        {
            patternTextures = new Dictionary<EPattern, Texture2D>(){
                { EPattern.A1, Resources.Load<Texture2D>($"{TEXTURE_PATH}/a") },
                { EPattern.BORROMEAN, Resources.Load<Texture2D>($"{TEXTURE_PATH}/borromean") },
                { EPattern.ENNEAGRAM, Resources.Load<Texture2D>($"{TEXTURE_PATH}/enneagram") },
                { EPattern.FLEURDEVIE, Resources.Load<Texture2D>($"{TEXTURE_PATH}/fleurdevie") },
                { EPattern.HEPTAGRAM, Resources.Load<Texture2D>($"{TEXTURE_PATH}/heptagram") },
                { EPattern.MAZE, Resources.Load<Texture2D>($"{TEXTURE_PATH}/maze") },
                { EPattern.OCTAGRAM, Resources.Load<Texture2D>($"{TEXTURE_PATH}/octagram") },
                { EPattern.CIRTRIANGLE, Resources.Load<Texture2D>($"{TEXTURE_PATH}/cirtriangle") },
                { EPattern.TRIFORCE, Resources.Load<Texture2D>($"{TEXTURE_PATH}/triangles") },
                { EPattern.GRID, Resources.Load<Texture2D>($"{TEXTURE_PATH}/grid") },
                { EPattern.SACRED, Resources.Load<Texture2D>($"{TEXTURE_PATH}/sacred") },
                { EPattern.SIERPINSKI, Resources.Load<Texture2D>($"{TEXTURE_PATH}/sierpinski") },
                { EPattern.SPIRAL, Resources.Load<Texture2D>($"{TEXTURE_PATH}/spiral") },
                { EPattern.YINYANG, Resources.Load<Texture2D>($"{TEXTURE_PATH}/yinyang") }
            };
            solutionsId = new();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (!IsClient)
                return;
            Debug.Log("CubeSubMiniGame OnNetworkSpawn - Client side");
            solutionsId.OnListChanged += _ => OnSolutionsReceived();
        }

        private void OnSolutionsReceived()
        {
            if (solutionsId.Count < NB_SOLUTIONS)
            {
                Debug.LogWarning("Solutions received are less than expected.");
                return;
            }

            Debug.LogWarning("Solutions received from server:");

            foreach (int id in solutionsId)
            {
                EPattern pattern = (EPattern)id;
                Debug.Log($"Received solution pattern: {pattern}");
            }
        }

        public void Init(bool canInit)
        {
            Debug.Log($"[Init] IsServer={IsServer}, IsClient={IsClient}, IsHost={IsHost}, IsSpawned={IsSpawned}",this);
            //destroy tous les cubes présents
            if (!canInit)
            {
                Debug.LogWarning("Only the red player can initialize the CubeSubMiniGame.");
                return;
            }

            DestroyPresentCubes();

            solutions = GetRandomPattern(NB_SOLUTIONS, onlyOnce: true, new List<EPattern>());
            solutionsId = new NetworkList<int>();
            foreach (var sol in solutions)
            {
                solutionsId.Add((int)sol);

            }

            //init desk rouge
            //  -> add solutions 0 à 3
            //init desk bleu
            //  -> add solutions 4 à 7
            var completeList = GetRandomPattern(NB_MAX_CUBES - NB_SOLUTIONS, onlyOnce: false, solutions);
            completeList.AddRange(solutions);
            completeList.Shuffle();

            //regénérer les solutions pour chaque joueurs
            //générer les cubes statiques avec la solution
            //générer les cubes dynamiques
            //  -> liste avec les 8 solutions (4 solutions * 2 joueurs)
            //  -> remplir la liste avec des cubes aléatoire qui ne sont pas dans les solutions jusqu'à la limite
            //instancier un par un les cubes dynamiques en alternant les zones à chaque instanciation
        }

        private void DestroyPresentCubes()
        {
            foreach (var cube in dynamicCubes)
            {
                Destroy(cube);
            }
            dynamicCubes.Clear();
            solutions.Clear();
        }

        private List<EPattern> GetRandomPattern(int length, bool onlyOnce, List<EPattern> exclude)
        {
            if (length >= patternTextures.Count && onlyOnce)
            {
                Debug.LogError("Number of solutions requested exceeds available patterns.");
                return null;
            }

            List<EPattern> allpatterns = new(patternTextures.Keys);
            List<EPattern> selectedSolutions = new();
            System.Random rnd = new();
            while (selectedSolutions.Count < length)
            {
                int index = rnd.Next(allpatterns.Count);
                EPattern key = allpatterns[index];
                if ((onlyOnce && !selectedSolutions.Contains(key) && (exclude == null || !exclude.Contains(key))) || (!onlyOnce))
                {
                    selectedSolutions.Add(key);
                }
            }
            return selectedSolutions;
        }
    }
}

