using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractablePrompt : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Arraste aqui o GameObject do ícone/prompt (no Canvas). Deve estar desativado por padrão.")]
    public GameObject promptUI;

    [Tooltip("Componente Text dentro do prompt que mostra a mensagem.")]
    public TextMeshProUGUI uiText;

    [Tooltip("Segundo prompt que deve aparecer somente quando o jogador interagir (opcional).")]
    public GameObject secondaryPrompt;

    [Header("Textos")]
    public string defaultText = "Pressione E para interagir";
    public string exitText = "Pressione E para sair";

    [Header("Config")]
    [Tooltip("Tag usada pelo jogador (padrão: Player)")]
    public string playerTag = "Player";

    // estado interno
    private bool isPlayerNearby = false;
    private bool isInteracting = false;

    void Awake()
    {
        // prompt principal visível apenas quando o jogador estiver perto
        if (promptUI != null)
            promptUI.SetActive(false);

        // o segundo prompt deve ficar escondido até o jogador iniciar a interação
        if (secondaryPrompt != null)
            secondaryPrompt.SetActive(false);

        SetPromptText(defaultText);
    }

    // Use OnTriggerEnter2D/Exit2D para 2D
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNearby = true;
            ShowPrompt(true);
            // não mostrar secondary aqui — só quando isInteracting == true
            SetPromptText(isInteracting ? exitText : defaultText);
            Debug.Log("Player entrou na área da estante: " + name);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNearby = false;
            // resetar estado de interação ao sair
            isInteracting = false;
            ShowPrompt(false);
            ShowSecondaryPrompt(false);
            SetPromptText(defaultText);
            Debug.Log("Player saiu da área da estante: " + name);
        }
    }

    // Se seu projeto for 3D, descomente estes métodos e comente os 2D acima
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNearby = true;
            ShowPrompt(true);
            SetPromptText(isInteracting ? exitText : defaultText);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNearby = false;
            isInteracting = false;
            ShowPrompt(false);
            ShowSecondaryPrompt(false);
            SetPromptText(defaultText);
        }
    }
    */

    private void ShowPrompt(bool show)
    {
        if (promptUI == null) return;
        promptUI.SetActive(show);
    }

    private void ShowSecondaryPrompt(bool show)
    {
        if (secondaryPrompt == null) return;
        secondaryPrompt.SetActive(show);
    }

    private void SetPromptText(string text)
    {
        if (uiText == null) return;
        uiText.text = text;
    }

    void Update()
    {
        if (!isPlayerNearby) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // alterna estado de interação
            isInteracting = !isInteracting;

            // atualiza texto do prompt conforme o estado
            SetPromptText(isInteracting ? exitText : defaultText);

            // o segundo prompt aparece somente quando isInteracting == true
            ShowSecondaryPrompt(isInteracting);

            // exemplo: aqui você pode abrir/fechar a UI de inspeção
            // if (isInteracting) OpenInspectUI(); else CloseInspectUI();

            Debug.Log("Tecla E pressionada enquanto perto da estante: " + name + " | Interagindo: " + isInteracting);
        }
    }

    void OnDisable()
    {
        if (promptUI != null)
            promptUI.SetActive(false);

        if (secondaryPrompt != null)
            secondaryPrompt.SetActive(false);
    }
}

