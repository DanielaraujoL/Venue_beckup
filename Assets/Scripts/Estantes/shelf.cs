using UnityEngine;

public class Shelf : MonoBehaviour
{
    [Header("Itens desta estante")]
    public Item[] shelfItems;

    // Método chamado pelo PlayerInteraction
    public void Interact()
    {
        ShelfUIManager.Instance.ShowShelfContents(shelfItems);
    }
}
