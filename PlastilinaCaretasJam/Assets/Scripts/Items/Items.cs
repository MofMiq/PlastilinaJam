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
}
