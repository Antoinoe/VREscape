using System.Collections.Generic;
using UnityEngine;
using XRMultiplayer.MiniGames;

public class CubeTest : MonoBehaviour
{
    public Texture2D tex;
    public EPattern pattern;
    public Dictionary<EPattern, Texture2D> patternTextures;
    private const string TEXTURE_PATH = "Assets/VRMPAssets/MiniGames/MiniGameTextures/Escape";

    private void Awake()
    {
        
    }

    private void Start()
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
    }

    public void Init(EPattern p)
    {

        pattern = p;
        var mr = GetComponent<MeshRenderer>();
        mr.material = new Material(mr.material.shader);
        //mr.material.mainTexture = patternTextures[pattern];
    }

}
