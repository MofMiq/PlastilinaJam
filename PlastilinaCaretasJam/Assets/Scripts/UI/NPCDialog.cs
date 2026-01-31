using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    public enum DialogPhase
    {
        Initial,
        Response
    }

    private DialogPhase currentPhase;


    public DialogUI dialogUI;

    int currentLineIndex = 0;

    [Header("Dialogo Inicial")]
    public string[] initialDialogLines;

    [Header("Dialogo Respuesta")]
    public string[] responseDialogLines;

    [Header("Inventario")]
    public CanvasGroup inventoryCanvasGroup;

    private void Start()
    {
        // Escuchamos cuando termina cualquier diálogo
        dialogUI.OnDialogFinished += OnDialogFinished;

        // Al empezar: diálogo inicial + inventario bloqueado
        SetInventoryInteractable(false);
        currentPhase = DialogPhase.Initial;
        dialogUI.StartDialog(initialDialogLines);
    }

    public void StartResponseDialog()
    {
        SetInventoryInteractable(false);
        dialogUI.StartDialog(responseDialogLines);
        dialogUI.StartDialog(responseDialogLines);
    }

    void OnDialogFinished()
    {
        currentLineIndex += 1;
        Debug.Log("Dialog finished" + currentLineIndex);
        // SetInventoryInteractable(true);
        Debug.Log("Dialog finished: " + currentPhase);

        if (currentPhase == DialogPhase.Initial)
        {
            SetInventoryInteractable(true);
            InventorySlotDebug.EnableAllSlots();
        }

          // OJO: el diálogo de respuesta NO reactiva inventario
    }

    

    void SetInventoryInteractable(bool value)
    {
        if (inventoryCanvasGroup == null) return;
        Debug.Log("SetInventoryInteractable: " + inventoryCanvasGroup);
        Debug.Log("SetInventoryInteractable to " + value);

        inventoryCanvasGroup.interactable = value;
        inventoryCanvasGroup.blocksRaycasts = value;
        inventoryCanvasGroup.alpha = value ? 1f : 0.5f;
    }
}
