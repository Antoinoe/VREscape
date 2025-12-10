using UnityEngine;

public class PlaqueScript : MonoBehaviour
{
    [SerializeField] private Material Red;
    [SerializeField] private Material Green;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
