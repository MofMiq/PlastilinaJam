using UnityEngine;
using UnityEngine.UI;

public class InventorySlotDebug : MonoBehaviour
{
    public MaskItem maskItem;
    public NPCMaskReceiver npcReceiver;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnSlotClicked()
    {
        if (maskItem == null || npcReceiver == null)
        {
            Debug.LogWarning("Slot not configured!");
            return;
        }

        // Intentamos dar la máscara
        npcReceiver.ReceiveMask(maskItem);

        // Desactivamos TODOS los slots (una sola entrega)
        DisableAllSlots();
    }

    void DisableAllSlots()
    {
        InventorySlotDebug[] allSlots = FindObjectsOfType<InventorySlotDebug>();
        foreach (var slot in allSlots)
        {
            if (slot.button != null)
                slot.button.interactable = false;
        }
    }
}
