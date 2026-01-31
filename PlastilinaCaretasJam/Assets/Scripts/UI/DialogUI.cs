using TMPro;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    public void SetText(string text)
    {
        dialogText.text = text;
    }
}
