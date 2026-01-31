using UnityEngine;

public class NPCMaskReceiver : MonoBehaviour
{
    public void ReceiveMask(MaskItem mask)
    {
        Debug.Log("NPC received mask: " + mask.maskName);

        // Aquí luego irá la lógica real de intercambio
    }
}
