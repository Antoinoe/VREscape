using System;
using UnityEngine;
using Random = System.Random;

public class PlaqueScript : MonoBehaviour
{
    [SerializeField] private Material Red;
    [SerializeField] private Material Green;
    [SerializeField] private Material Gray;
    private CubeScript.ESymbol symbol;
    private bool isMatching;

    public CubeScript.ESymbol Symbol
    {
        get { return symbol; }
        set { symbol = value; }
    }

    public bool IsMatching
    {
        get { return isMatching; }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlaqueDetect child = GetComponentInChildren<PlaqueDetect>();
        child.OnChildTriggerEnter += ChildTriggerEnter;
        child.OnChildTriggerExit += ChildTriggerExit;
        //Random rnd = new Random();
        //int value = rnd.Next(0, Enum.GetValues(typeof(CubeScript.ESymbol)).Length);
        //symbol = (Symbol)value;
        //symbol = CubeScript.ESymbol.A1;
    }
    
    private void ChildTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            Vector3 position = transform.position;
            //position.y += transform.position.y/2 + other.transform.position.y / 2;
            other.transform.position = position;
            other.transform.rotation = Quaternion.identity;
            if (symbol == other.GetComponent<CubeScript>().Symbol)
            {
                GetComponent<Renderer>().material = Green;
                isMatching = true;
            }
            else
            {
                GetComponent<Renderer>().material = Red;
                isMatching = false;
            }
            Debug.Log("enter");
        }
    }
    
    private void ChildTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            GetComponent<Renderer>().material = Gray;
            isMatching = false;
            Debug.Log("exit");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            GetComponent<Renderer>().material = Green;
            Debug.Log("enter");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            GetComponent<Renderer>().material = Red;
            Debug.Log("exit");
        }
    }

}
