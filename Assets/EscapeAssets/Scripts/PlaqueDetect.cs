using UnityEngine;

public class PlaqueDetect : MonoBehaviour
{
    public System.Action<Collider> OnChildTriggerEnter;
    public System.Action<Collider> OnChildTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        OnChildTriggerEnter?.Invoke(other);
    }
    
    private void OnTriggerExit(Collider other)
    {
        OnChildTriggerExit?.Invoke(other);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
