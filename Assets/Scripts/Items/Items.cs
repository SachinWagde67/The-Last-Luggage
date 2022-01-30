using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/newItem")]
public class Items : ScriptableObject
{
    public string itemName;
    public Sprite icon;
}
