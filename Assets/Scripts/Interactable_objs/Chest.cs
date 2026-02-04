using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public GameObject ChestPrefab;
    
    public void Interact()
    {
        Debug.Log("Chest opened!");
        // Add chest opening logic here
    }
}
