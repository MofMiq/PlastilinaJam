using UnityEngine;
using UnityEngine.UI;

public class InventorySlotDebug : MonoBehaviour
{
    public MaskItem maskItem;
    public NPCMaskReceiver npcReceiver;

    public Image iconImage;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        RefreshVisual();
    }

    void RefreshVisual()
    {
        if (maskItem != null && iconImage != null)
        {
            iconImage.sprite = maskItem.icon;
            iconImage.enabled = true;
        }
    }

    public void OnSlotClicked()
    {
        if (maskItem == null || npcReceiver == null)
            return;

        // Dar máscara al NPC y recibir recompensa
        MaskItem reward = npcReceiver.ReceiveMask(maskItem);

        if (reward != null)
        {
            Debug.Log("Slot swapped to new mask: " + reward.maskName);

            // SWAP REAL
            maskItem = reward;
            RefreshVisual();
        }

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
