using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Referência ao Canvas de inspeção")]
    public GameObject inspectCanvas; // arraste o Canvas no Inspector

    private bool isPlayerNearby = false;

    void Update()
    {
        // Se o jogador estiver perto e pressionar "E"
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleInspect();
        }
    }

    void ToggleInspect()
    {
        if (inspectCanvas == null)
        {
            Debug.LogWarning("Canvas de inspeção não atribuído no Inspector!");
            return;
        }

        bool isActive = inspectCanvas.activeSelf;
        inspectCanvas.SetActive(!isActive);

        // Pausar o jogo enquanto inspeciona
        Time.timeScale = isActive ? 1f : 0f;

        Debug.Log("Canvas " + (isActive ? "fechado" : "aberto"));
    }

    // Use 2D se estiver usando Rigidbody2D
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player entrou na área de interação");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player saiu da área de interação");
        }
    }
}
