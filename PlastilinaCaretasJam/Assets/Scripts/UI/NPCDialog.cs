using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    public DialogUI dialogUI;

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
        dialogUI.StartDialog(initialDialogLines);
    }

    public void StartResponseDialog()
    {
        SetInventoryInteractable(false);
        dialogUI.StartDialog(responseDialogLines);
    }

    void OnDialogFinished()
    {
        Debug.Log("Dialog finished");
        SetInventoryInteractable(true);
    }

    void SetInventoryInteractable(bool value)
    {
        if (inventoryCanvasGroup == null) return;

        inventoryCanvasGroup.interactable = value;
        inventoryCanvasGroup.blocksRaycasts = value;
        inventoryCanvasGroup.alpha = value ? 1f : 0.5f;
    }
}
