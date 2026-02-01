using UnityEngine;
using UnityEngine.UI;

public class TransitionButton : MonoBehaviour
{
    private Button button;
    
    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonClicked);
            Debug.Log("TransitionButton: Botón registrado correctamente");
            Debug.Log($"TransitionButton: Interactuable = {button.interactable}");
        }
        else
        {
            Debug.LogError("TransitionButton: No se encontró componente Button");
        }
    }
    
    void OnButtonClicked()
    {
        Debug.Log("TransitionButton: OnButtonClicked fue llamado!");
        Debug.Log($"Próxima escena guardada: {TransitionManager.GetNextScene()}");
        TransitionManager.LoadNextScene();
    }
}
