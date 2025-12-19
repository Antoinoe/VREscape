using System;
using UnityEngine;
using UnityEngine.UIElements;
using XRMultiplayer.MiniGames;

public class Desk : MonoBehaviour
{
    [SerializeField] private MiniGame_Escape parentMiniGame;
    [SerializeField] private GameObject Plaque;
    private GameObject[] Plaques = new GameObject[4];
    [SerializeField] private DeskExemple deskExemple;
    private bool isWin;
    private bool isInit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Init()
    {
        int index = 0;
        Vector3 PlaquePosition = transform.position;
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
                Plaques[index].GetComponent<PlaqueScript>().symbol = deskExemple.cubes[index].GetComponent<CubeTest>().pattern;
            }
        }
        isInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInit)
            return;

        for (int i = 0; i < Plaques.Length; i++)
        {
            if (!Plaques[i].GetComponent<PlaqueScript>().IsMatching)
            {
                return;
            }
        }
        parentMiniGame.ChangeState(EscapeGameState.Exit);
    }
}
