using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlotDebug : MonoBehaviour
{
    public MaskItem maskItem;
    public NPCMaskReceiver npcReceiver;

    public Image iconImage;
    private Button button;
    public int slotIndex; // Índice del slot para identificarlo

    void Awake()
    {
        button = GetComponent<Button>();
        LoadFromGameManager();
        RefreshVisual();
    }

    void LoadFromGameManager()
    {
        if (GameManager.Instance != null && GameManager.Instance.savedInventory.Count > slotIndex)
        {
            maskItem = GameManager.Instance.savedInventory[slotIndex];
        }
    }

    void RefreshVisual()
    {
        if (maskItem != null && iconImage != null)
        {
            iconImage.sprite = maskItem.icon;
            iconImage.enabled = true;
        }
        if (maskItem.name == "InvisibleMask")
        {
            iconImage.enabled = false;
        }
    }

    public void OnSlotClicked()
    {
        // Debug.Log("Slot clicked with mask: " + (maskItem != null ? maskItem.maskName : "None"));
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
            Debug.Log("Disabled slot with mask: " + (slot.maskItem != null ? slot.maskItem.maskName : "None"));
        }
        Debug.Log("All inventory slots disabled.");

        // Guardar inventario en GameManager
        SaveInventoryToGameManager();
    }

    void SaveInventoryToGameManager()
    {
        if (GameManager.Instance == null) return;

        InventorySlotDebug[] allSlots = FindObjectsOfType<InventorySlotDebug>();
        List<MaskItem> masks = new List<MaskItem>();

        // Ordenar por slotIndex para mantener el orden
        System.Array.Sort(allSlots, (a, b) => a.slotIndex.CompareTo(b.slotIndex));

        foreach (var slot in allSlots)
        {
            masks.Add(slot.maskItem);
        }

        GameManager.Instance.SaveInventory(masks);
    }
}
