using UnityEngine;
using UnityEngine.Serialization;
using XRMultiplayer.MiniGames;
using System.Collections.Generic;

public class DeskExemple : MonoBehaviour
{
    [FormerlySerializedAs("Cube")] [SerializeField] private GameObject Cube;
    private GameObject[] Cubes = new GameObject[4];

    public GameObject[] cubes
    {
        get => Cubes;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Init(List<int> solutions)
    {
        int index = 0;
        Vector3 CubePosition = transform.position;
        CubePosition.x -= transform.localScale.x * 0.5f - Cube.transform.localScale.x * 0.5f;
        CubePosition.y += transform.localScale.y * 0.5f + Cube.transform.localScale.y * 0.5f;
        CubePosition.z -= transform.localScale.z * 0.5f - Cube.transform.localScale.z * 0.5f;
        Vector3 OriginalPosition = CubePosition;
        for (int i = 0; i < 2; i++)
        {
            CubePosition.x = OriginalPosition.x + (0.5f + 2 * i) * Cube.transform.localScale.x;
            for (int j = 0; j < 2; j++, index++)
            {
                CubePosition.z = OriginalPosition.z + (0.5f + 2 * j) * Cube.transform.localScale.z;
                Cubes[index] = Instantiate(Cube, CubePosition, Quaternion.identity);
                Debug.Log($"Cube {index} {CubePosition}");
                var ct = Cubes[index].GetComponent<CubeTest>();
                ct.Init((EPattern)index);
                //Cubes[index].GetComponent<CubeTest>().IsLocked = true;
                //Cubes[index].transform.SetParent(transform);
            }
        }
    }
}
