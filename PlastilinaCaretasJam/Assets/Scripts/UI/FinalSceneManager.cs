using UnityEngine;
using UnityEngine.UI;

public class FinalSceneManager : MonoBehaviour
{
    [Header("Animator Final")]
    public Animator finalAnimator;

    [Header("Animators de Finales")]
    public RuntimeAnimatorController finalBad;       // Score muy bajo
    public RuntimeAnimatorController finalNormal;    // Score bajo-medio
    public RuntimeAnimatorController finalGood;      // Score medio-alto
    public RuntimeAnimatorController finalPerfect;   // Score máximo

    [Header("Música de Finales")]
    public AudioSource musicSource;
    public AudioClip musicBad;
    public AudioClip musicNormal;
    public AudioClip musicGood;
    public AudioClip musicPerfect;

    [Header("Rangos de Score")]
    public int thresholdNormal = 4;   // >= para normal
    public int thresholdGood = 8;   // >= para bueno
    public int thresholdPerfect = 12; // >= para perfecto

    void Start()
    {
        LoadFinalBasedOnScore();
    }

    void LoadFinalBasedOnScore()
    {
        int score = 0;

        if (GameManager.Instance != null)
        {
            score = GameManager.Instance.totalScore;
            Debug.Log("Score final: " + score);
        }
        else
        {
            Debug.LogWarning("GameManager no encontrado. Usando score 0.");
        }

        // Seleccionar animator según el score
        RuntimeAnimatorController selectedAnimator = GetFinalAnimator(score);

        if (finalAnimator != null && selectedAnimator != null)
        {
            finalAnimator.runtimeAnimatorController = selectedAnimator;
        }
    }

    void SetScale(float x, float y)
    {
        finalAnimator.transform.localScale = new Vector3(x, y, 1f);
    }

    void SetPosition(float x, float y)
    {
        finalAnimator.transform.position = new Vector3(x, y, 0f);
    }

    RuntimeAnimatorController GetFinalAnimator(int score)
    {
        if (score >= thresholdPerfect)
        {
            Debug.Log("Final: PERFECTO");
            SetScale(1.06f, 1.06f);
            SetPosition(-4.5f, -0.6f);
            PlayMusic(musicPerfect);
            return finalPerfect;
        }
        else if (score >= thresholdGood)
        {
            Debug.Log("Final: BUENO");
            SetPosition(-3f, -1f);
            SetScale(0.9f, 0.9f);
            PlayMusic(musicGood);
            return finalGood;
        }
        else if (score >= thresholdNormal)
        {
            Debug.Log("Final: NORMAL");
            SetPosition(-3f, -1f);
            SetScale(0.9f, 0.9f);
            PlayMusic(musicNormal);
            return finalNormal;
        }
        else
        {
            Debug.Log("Final: MALO");
            SetPosition(0f, 0f);
            SetScale(0.7f, 0.7f);
            PlayMusic(musicBad);
            return finalBad;
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}
