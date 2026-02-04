using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private LayerMask interactableLayer;
    
    private InputAction interactAction;
    private IInteractable nearestInteractable;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        FindNearestInteractable();
        
        if (interactAction.WasPressedThisFrame() && nearestInteractable != null)
        {
            nearestInteractable.Interact();
        }
    }

    void FindNearestInteractable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange, interactableLayer);
        
        nearestInteractable = null;
        float closestDistance = interactionRange;
        
        foreach (Collider col in colliders)
        {
            IInteractable interactable = col.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestInteractable = interactable;
                }
            }
        }
    }
}
