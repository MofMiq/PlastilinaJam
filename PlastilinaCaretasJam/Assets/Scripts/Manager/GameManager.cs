using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalScore = 0;

    // Inventario persistente entre escenas
    public List<MaskItem> savedInventory = new List<MaskItem>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        totalScore += value;
        Debug.Log("SCORE TOTAL = " + totalScore);
    }

    public void SaveInventory(List<MaskItem> masks)
    {
        savedInventory = new List<MaskItem>(masks);
        Debug.Log("Inventario guardado con " + savedInventory.Count + " máscaras.");
    }

    public List<MaskItem> GetSavedInventory()
    {
        return savedInventory;
    }
}