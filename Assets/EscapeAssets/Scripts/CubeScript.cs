using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Random = System.Random;

public class CubeScript : MonoBehaviour
{
    private ESymbol symbol;
    private Material material;
    private bool isLocked = false;
    
    public enum ESymbol
    {
        borromean,
        A1,
        Rond,
        Carré,
        Triangle
    }

    public bool IsLocked
    {
        get => isLocked;
        set => isLocked = value;
    }

    public ESymbol Symbol
    {
        get => symbol;
        set => symbol = value;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Random rnd = new Random();
        int value = rnd.Next(0, Enum.GetValues(typeof(ESymbol)).Length);
        //symbol = (Symbol)value;
        var renderer = GetComponent<Renderer>();
        renderer.material = new Material(renderer.material.shader);
        string path = "Textures/";
        path = path + symbol.ToString();
        renderer.material.mainTexture = Resources.Load<Texture2D>(path);

        if (isLocked)
        {
            Destroy(GetComponent<Rigidbody>());
            var interactable = GetComponent<XRGrabInteractable>();
            if (interactable != null)
            {
                interactable.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
