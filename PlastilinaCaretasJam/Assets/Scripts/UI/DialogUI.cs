using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class DialogUI : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    [Header("Efecto de Escritura")]
    [Tooltip("Caracteres por segundo")]
    public float typewriterSpeed = 30f;

    private string[] currentLines;
    private int currentIndex;
    private bool isTyping = false;
    private Coroutine typewriterCoroutine;

    public Action OnDialogFinished;

    public void StartDialog(string[] lines)
    {   
        if (lines == null || lines.Length == 0)
        {
            FinishDialog();
            return;
        }

        currentLines = lines;
        currentIndex = 0;
        ShowCurrentLine();
    }

    public void NextLine()
    {
        // Si aún se está escribiendo, completar el texto inmediatamente
        if (isTyping)
        {
            CompleteCurrentLine();
            return;
        }

        // Si ya terminó de escribir, pasar a la siguiente línea
        currentIndex++;

        if (currentIndex >= currentLines.Length)
        {
            FinishDialog();
            return;
        }

        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        typewriterCoroutine = StartCoroutine(TypewriterEffect(currentLines[currentIndex]));
    }

    void CompleteCurrentLine()
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        dialogText.text = currentLines[currentIndex];
        isTyping = false;
    }

    IEnumerator TypewriterEffect(string line)
    {
        isTyping = true;
        dialogText.text = "";

        int i = 0;
        while (i < line.Length)
        {
            // Si encontramos una etiqueta, añadirla completa
            if (line[i] == '<')
            {
                int closeIndex = line.IndexOf('>', i);
                if (closeIndex != -1)
                {
                    // Añadir toda la etiqueta de golpe
                    dialogText.text += line.Substring(i, closeIndex - i + 1);
                    i = closeIndex + 1;
                    continue;
                }
            }

            // Añadir carácter normal
            dialogText.text += line[i];
            i++;
            yield return new WaitForSeconds(1f / typewriterSpeed);
        }

        isTyping = false;
    }

    void FinishDialog()
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        dialogText.text = "";
        isTyping = false;
        OnDialogFinished?.Invoke();
    }
}
