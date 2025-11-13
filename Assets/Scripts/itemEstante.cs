using UnityEngine;
using UnityEngine.UI;

public class ItemEstante : MonoBehaviour
{
    [Header("Referências de UI")]
    public GameObject inspectCanvas;
    public Image itemImageUI;
    public Text itemDescriptionUI;

    [Header("Dados do item")]
    public Sprite itemSprite;
    [TextArea]
    public string itemDescription;

    public void Inspecionar()
    {
        inspectCanvas.SetActive(true);
        itemImageUI.sprite = itemSprite;
        itemDescriptionUI.text = itemDescription;
        Time.timeScale = 0f;
    }

    void OnMouseDown()
    {
        Inspecionar();
    }

}
