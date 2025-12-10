using UnityEngine;
using UnityEngine.UIElements;

public class Desk : MonoBehaviour
{
    [SerializeField] private GameObject Plaque;
    private GameObject[] Plaques = new GameObject[9];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int index = 0;
        Vector3 PlaquePosition  = transform.position;
        PlaquePosition.x -= transform.localScale.x * 0.5f - Plaque.transform.localScale.x * 0.5f;
        PlaquePosition.y += transform.localScale.y * 0.5f;
        PlaquePosition.z -= transform.localScale.z * 0.5f - Plaque.transform.localScale.z * 0.5f;
        Vector3 OriginalPosition = PlaquePosition;
        for (int i = 0; i < 3; i++)
        {
            PlaquePosition.x = OriginalPosition.x + i * Plaque.transform.localScale.x;
            for (int j = 0; j < 3; j++, index++)
            {
                PlaquePosition.z = OriginalPosition.z + j * Plaque.transform.localScale.z;
                Plaques[index] = Instantiate(Plaque, PlaquePosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
