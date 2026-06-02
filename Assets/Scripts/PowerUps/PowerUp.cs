using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PowerUp")]
public class PowerUp : ScriptableObject
{
    public string itemName = "defaultName";

    public Sprite powerUpIcon;

    public float duration;

    public virtual void Use()
    {
        Debug.Log($"Used Standard Item: {itemName}");
    }
}
