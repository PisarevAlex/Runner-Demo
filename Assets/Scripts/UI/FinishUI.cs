using UnityEngine;

public class FinishUI : MonoBehaviour
{
    public void LoadNextLevel()
    {
        FindObjectOfType<LevelManager>().LoadNextLevel();
    }
}
