using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    float interactionDistance = 5f;
    Interactable currentTargetedInteractable;

    private void Update()
    {
        UpdateCurrentInteractable();
        CheckForInteractionInput();
    }

    private void UpdateCurrentInteractable()
    {
        var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        Physics.Raycast(ray, out var hit, interactionDistance);
        currentTargetedInteractable = hit.collider?.GetComponent<Interactable>();
    }

    private void CheckForInteractionInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentTargetedInteractable != null)
        {
            currentTargetedInteractable.OnInteract();
        }
    }
}
