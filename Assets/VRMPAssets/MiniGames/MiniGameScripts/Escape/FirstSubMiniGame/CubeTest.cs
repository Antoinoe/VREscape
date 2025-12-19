using System.Collections.Generic;
using UnityEngine;
using XRMultiplayer.MiniGames;

public class CubeTest : MonoBehaviour
{
    public Texture2D tex;
    public EPattern pattern;
    public Dictionary<EPattern, string> patternTextures;
    private const string TEXTURE_PATH = "Assets/VRMPAssets/MiniGames/MiniGameTextures/Escape";

    private void Awake()
    {
        patternTextures = new Dictionary<EPattern, string>(){
                { EPattern.A1, "a" },
                { EPattern.BORROMEAN, "borromean"},
                { EPattern.ENNEAGRAM, "enneagram" },
                { EPattern.FLEURDEVIE, "fleurdevie" },
                { EPattern.HEPTAGRAM, "heptagram"},
                { EPattern.MAZE, "maze" },
                { EPattern.OCTAGRAM, "octagram"},
                { EPattern.CIRTRIANGLE, "cirtriangle" },
                { EPattern.TRIFORCE, "triangles" },
                { EPattern.GRID, "grid" },
                { EPattern.SACRED, "sacred" },
                { EPattern.SIERPINSKI, "sierpinski" },
                { EPattern.SPIRAL, "spiral" },
                { EPattern.YINYANG, "yinyang" }
            };
    }

    public void Init(EPattern p)
    {

        pattern = p;
        //var mr = GetComponent<MeshRenderer>();
        //mr.material = new Material(mr.material.shader);

        //var renderer = GetComponent<Renderer>();
        //renderer.material = new Material(renderer.material.shader);
        //string path = "Assets/VRMPAssets/MiniGames/MiniGameTextures/Escape/";
        //path = path + patternTextures[pattern];
        //Debug.LogError($"PATH TEX FOR {patternTextures[pattern]} : {path}");
        //renderer.material.mainTexture = Resources.Load<Texture2D>(path);
    }

}
