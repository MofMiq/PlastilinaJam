using UnityEngine;

[CreateAssetMenu(menuName = "Game/Mask Item")]
public class MaskItem : ScriptableObject
{
    public string maskName;
    public Sprite icon;
    public Sprite npcSprite;

    public Sprite orugaSprite;

    public Sprite cuneiformeSprite;

    public Sprite pescaoSprite;
    // Valores por NPC
    public int oruga;
    public int cuneiforme;
    public int pescao;

    public string responseOruga;
    public string responseCuneiforme;
    public string responsePescao;

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

    public string GetResponseForNPC(string npcName)
    {
        switch (npcName)
        {
            case "Oruga":
                return responseOruga;

            case "Cuneiforme":
                return responseCuneiforme;

            case "Pescao":
                return responsePescao;

            default:
                Debug.LogWarning("NPC no reconocido: " + npcName);
                return "";
        }
    }

    public Sprite GetSpriteForNPC(string npcName)
    {
        switch (npcName)
        {
            case "Oruga":
                return orugaSprite;

            case "Cuneiforme":
                return cuneiformeSprite;

            case "Pescao":
                return pescaoSprite;

            default:
                Debug.LogWarning("NPC no reconocido: " + npcName);
                return npcSprite; // Devuelve el sprite por defecto
        }
    }
}
