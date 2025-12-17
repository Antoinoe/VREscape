using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Desk : MonoBehaviour
{
    [SerializeField] private GameObject Plaque;
    private GameObject[] Plaques = new GameObject[4];
    [SerializeField] private DeskExemple deskExemple;
    private bool isWin;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int index = 0;
        Vector3 PlaquePosition  = transform.position;
        PlaquePosition.x -= transform.localScale.x * 0.5f - Plaque.transform.localScale.x * 0.5f;
        PlaquePosition.y += transform.localScale.y * 0.5f;
        PlaquePosition.z -= transform.localScale.z * 0.5f - Plaque.transform.localScale.z * 0.5f;
        Vector3 OriginalPosition = PlaquePosition;
        for (int i = 0; i < 2; i++)
        {
            PlaquePosition.x = OriginalPosition.x + i * Plaque.transform.localScale.x;
            for (int j = 0; j < 2; j++, index++)
            {
                PlaquePosition.z = OriginalPosition.z + j * Plaque.transform.localScale.z;
                Plaques[index] = Instantiate(Plaque, PlaquePosition, Quaternion.identity);
                Plaques[index].transform.SetParent(transform);
                Plaques[index].GetComponent<PlaqueScript>().Symbol = deskExemple.cubes[index].GetComponent<CubeScript>().Symbol;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool temp = true;
        for (int i = 0; i < Plaques.Length; i++)
        {
            if (!Plaques[i].GetComponent<PlaqueScript>().IsMatching)
            {
                temp = false;
            }
        }
        isWin = temp;
        Console.WriteLine(isWin);
    }
}
