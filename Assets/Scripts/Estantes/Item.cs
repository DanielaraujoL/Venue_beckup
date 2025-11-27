using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;       // Nome do item
    public Sprite itemIcon;       // Ícone do item
    [TextArea] public string description; // Descrição opcional
}

