using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Componente para mostrar texto con efecto typewriter.
/// Añádelo al mismo GameObject que tiene el componente TextMeshProUGUI.
/// </summary>
public class TypewriterText : MonoBehaviour
{
    [Header("Texto")]
    [TextArea(3, 10)]
    public string fullText;

    [Header("Configuración")]
    [Tooltip("Caracteres por segundo")]
    public float typewriterSpeed = 30f;

    [Tooltip("Empezar automáticamente al iniciar")]
    public bool playOnStart = true;

    private TextMeshProUGUI textComponent;
    private Coroutine typewriterCoroutine;
    private bool isTyping = false;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
        {
            Debug.LogError("TypewriterText: No se encontró componente TextMeshProUGUI en este GameObject.");
            return;
        }

        // Si el texto está vacío, usar el texto actual del componente
        if (string.IsNullOrEmpty(fullText))
        {
            fullText = textComponent.text;
        }

        if (playOnStart)
        {
            StartTypewriter();
        }
    }

    public void StartTypewriter()
    {
        if (textComponent == null) return;

        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        typewriterCoroutine = StartCoroutine(TypewriterEffect());
    }

    public void CompleteText()
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        textComponent.text = fullText;
        isTyping = false;
    }

    IEnumerator TypewriterEffect()
    {
        isTyping = true;
        textComponent.text = "";

        int i = 0;
        while (i < fullText.Length)
        {
            // Si encontramos una etiqueta, añadirla completa
            if (fullText[i] == '<')
            {
                int closeIndex = fullText.IndexOf('>', i);
                if (closeIndex != -1)
                {
                    // Añadir toda la etiqueta de golpe
                    textComponent.text += fullText.Substring(i, closeIndex - i + 1);
                    i = closeIndex + 1;
                    continue;
                }
            }

            // Añadir carácter normal
            textComponent.text += fullText[i];
            i++;
            yield return new WaitForSeconds(1f / typewriterSpeed);
        }

        isTyping = false;
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}
