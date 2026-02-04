using UnityEngine;

public class DebugTester : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    private const int FishPoints = 1;
    private const int FishEvidencePoints = 5;
    private const int FishCorpsePoints = 10;
    
    public void AddFish()
    {
        scoreManager?.AddScore(FishPoints);
    }

    public void AddFishEvidence()
    {
        scoreManager?.AddScore(FishEvidencePoints);
    }

    public void AddFishCorpse()
    {
        scoreManager?.AddScore(FishCorpsePoints);
    }

    public void ResetCombo()
    {
        scoreManager?.ResetCombo();
       }
}