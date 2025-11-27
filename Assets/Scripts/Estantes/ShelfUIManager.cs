using UnityEngine;
using UnityEngine.UI;

public class ShelfUIManager : MonoBehaviour
{
    public static ShelfUIManager Instance;

    [Header("Referências da UI")]
    [SerializeField] private GameObject uiPanel;       // Painel da UI
    [SerializeField] private Transform itemContainer;  // Onde os itens aparecem
    [SerializeField] private GameObject itemPrefab;    // Prefab de botão/item

    private void Awake()
    {
        Instance = this;
    }

    public void ShowShelfContents(Item[] items)
    {
        // Limpa itens antigos
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // Cria botões para cada item
        foreach (Item item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, itemContainer);

            // Configura texto e ícone
            newItem.GetComponentInChildren<Text>().text = item.itemName;
            newItem.GetComponentInChildren<Image>().sprite = item.itemIcon;

            // Adiciona interação
            Button btn = newItem.GetComponent<Button>();
            btn.onClick.AddListener(() => OnItemClicked(item));
        }

        uiPanel.SetActive(true);
    }

    private void OnItemClicked(Item item)
    {
        Debug.Log("Você clicou no item: " + item.itemName);
        // Aqui você pode abrir descrição, usar o item, etc.
    }

    public void CloseUI()
    {
        uiPanel.SetActive(false);
    }
}

