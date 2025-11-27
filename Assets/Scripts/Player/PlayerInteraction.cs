using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuração")]
    public float interactionRange = 3f; // Distância máxima para interagir
    public KeyCode interactionKey = KeyCode.E;

    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Shelf shelf = hit.collider.GetComponent<Shelf>();
            if (shelf != null)
            {
                shelf.Interact();
            }
        }
    }
}
