using TMPro;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [Header("Dados do Card")]
    public string titulo;
    public string status;
    public string descricao;
    public int id;
    public bool inArea = false;
    public bool inCorrectArea = false;

    public Tarefa tarefa;

    [Header("UI Modal")]
    public TextMeshProUGUI tituloModal;
    public TextMeshProUGUI descModal;
    public GameObject modalPanel; // opcional: painel que será ativado/desativado
    public bool concluido;

    [Header("Sprites")]
    public Sprite spriteCorreto;
    public Sprite spriteErrado;
    public Sprite spriteNeutro;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogWarning($"CardController: SpriteRenderer não encontrado em {name}");

        // garante estado visual inicial
        ApplySpriteState();
        if (modalPanel != null) modalPanel.SetActive(false);
    }

    public void ConfigurarCard(Tarefa tarefa)
    {
        if (tarefa == null) return;

        this.tarefa = tarefa;
        titulo = tarefa.titulo;
        status = tarefa.status;
        descricao = tarefa.textoDaConclusao;
        id = tarefa.id;
        inArea = false;
        inCorrectArea = false;
        concluido = false;

        ApplySpriteState();
    }

    // Recomendo usar trigger para áreas de interação (mais simples para UI)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inArea = true;
            // atualiza modal se estiver atribuído
            if (tituloModal != null) tituloModal.text = titulo;
            if (descModal != null) descModal.text = descricao;

            // se você quiser abrir automaticamente o modal ao entrar:
            // OpenModal();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inArea = false;
            // fechar modal ao sair (opcional)
            // CloseModal();
        }
    }

    // Se você realmente precisa de colisão física, mantenha este método
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // comportamento alternativo: apenas atualiza textos
            if (tituloModal != null) tituloModal.text = titulo;
            if (descModal != null) descModal.text = descricao;
        }
    }

    // Abre o painel modal (se atribuído)
    public void OpenModal()
    {
        if (modalPanel != null)
            modalPanel.SetActive(true);

        if (tituloModal != null) tituloModal.text = titulo;
        if (descModal != null) descModal.text = descricao;
    }

    // Fecha o painel modal (se atribuído)
    public void CloseModal()
    {
        if (modalPanel != null)
            modalPanel.SetActive(false);
    }

    // Atualiza sprite para correto/errado/neutro conforme flags
    public void ApplySpriteState()
    {
        if (spriteRenderer == null) return;

        if (concluido)
        {
            spriteRenderer.sprite = spriteCorreto != null ? spriteCorreto : spriteNeutro;
            return;
        }

        if (inCorrectArea)
        {
            spriteRenderer.sprite = spriteCorreto != null ? spriteCorreto : spriteNeutro;
        }
        else if (inArea && !inCorrectArea)
        {
            spriteRenderer.sprite = spriteErrado != null ? spriteErrado : spriteNeutro;
        }
        else
        {
            spriteRenderer.sprite = spriteNeutro;
        }
    }

    public void SetSpriteCorreto()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = spriteCorreto;
        concluido = true;
    }

    public void SetSpriteErrado()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = spriteErrado;
        concluido = false;
    }

    public void SetSpriteNeutro()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = spriteNeutro;
        concluido = false;
    }

    // Chamado quando o card é colocado na área correta (por outro sistema)
    public void MarkAsCorrectArea()
    {
        inCorrectArea = true;
        ApplySpriteState();
    }

    // Chamado quando o card é removido da área correta
    public void UnmarkCorrectArea()
    {
        inCorrectArea = false;
        ApplySpriteState();
    }
}
