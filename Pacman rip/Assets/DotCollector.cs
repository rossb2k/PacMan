using UnityEngine;

public class DotCollector : MonoBehaviour
{
    public ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("DotCollector: ScoreManager not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (scoreManager != null)
            {
                scoreManager.CollectDot();
            }
            else
            {
                Debug.LogWarning("DotCollector: ScoreManager is not assigned.");
            }

            Destroy(gameObject);
        }
    }
}
