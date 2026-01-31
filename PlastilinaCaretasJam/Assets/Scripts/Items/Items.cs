using UnityEngine;

[CreateAssetMenu(menuName = "Game/Mask Item")]
public class MaskItem : ScriptableObject
{
    public string maskName;
    public Sprite icon;
    public Sprite npcSprite;

    // Valores por NPC
    public int oruga;
    public int cuneiforme;
    public int pescao;

    // 🔽 MÉTODO CLAVE
    public int GetValueForNPC(string npcName)
    {
        switch (npcName)
        {
            case "Oruga":
                return oruga;

            case "Cuneiforme":
                return cuneiforme;

            case "Pescao":
                return pescao;

            default:
                Debug.LogWarning("NPC no reconocido: " + npcName);
                return 0;
        }
    }
}
