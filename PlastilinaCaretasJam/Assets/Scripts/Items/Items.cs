using UnityEngine;

[CreateAssetMenu(menuName = "Game/Mask Item")]
public class MaskItem : ScriptableObject
{
    public string maskName;
    public Sprite icon;

    // Campos que luego usarán los NPC
    public int kindness;
    public int rarity;
    public int darkness;
}
