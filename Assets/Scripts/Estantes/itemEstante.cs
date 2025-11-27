using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEstante : MonoBehaviour
{
    [Header("UI")]
    public GameObject inspectCanvas;
    public Image itemImageUI;
    public TextMeshProUGUI itemDescriptionUI;

    [Header("Dados do item")]
    public Sprite itemSprite;
    [TextArea]
    public string itemDescription;

    [Header("Estado")]
    public string itemID; // id única para salvar estado
    public bool isCollected = false;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        // Carregar estado salvo
        isCollected = PlayerPrefs.GetInt("collected_" + itemID, 0) == 1;
        ApplyVisualState();
    }

    void OnMouseDown()
    {
        Debug.Log("Item clicado: " + name);
        inspectCanvas.SetActive(true);
        itemImageUI.sprite = itemSprite;
        itemDescriptionUI.text = itemDescription;
        Time.timeScale = 0f;
    }

    // Chame quando o jogador "coletar" o item (por botão, arrastar, etc.)
    public void Collect()
    {
        if (isCollected) return;
        isCollected = true;
        PlayerPrefs.SetInt("collected_" + itemID, 1);
        PlayerPrefs.Save();
        ApplyVisualState();
    }

    private void ApplyVisualState()
    {
        if (sr == null) return;

        if (isCollected)
        {
            // volta à cor normal
            sr.color = Color.white;
        }
        else
        {
            // deixa cinza
            sr.color = Color.gray;
        }
    }
}
