using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalScore = 0;

    [Header("NPC Flow")]
    public NPCController[] npcs;   // Array de NPCs en orden
    private int currentNPCIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCurrentNPC();
    }

    void StartCurrentNPC()
    {
        if (currentNPCIndex >= npcs.Length)
        {
            Debug.Log("TODOS LOS NPC COMPLETADOS");
            OnAllNPCsFinished();
            return;
        }

        Debug.Log("Activando NPC: " + npcs[currentNPCIndex].name);

        npcs[currentNPCIndex].ActivateNPC();
    }

    public void OnNPCCompleted()
    {
        Debug.Log("NPC completado");

        currentNPCIndex++;
        StartCurrentNPC();
    }

    public void AddScore(int value)
    {
        totalScore += value;
        Debug.Log("SCORE TOTAL = " + totalScore);
    }

    void OnAllNPCsFinished()
    {
        Debug.Log("FIN DEL JUEGO / DECIDIR FINAL");
        // aquí luego ponéis finales, escenas, etc
    }
}
