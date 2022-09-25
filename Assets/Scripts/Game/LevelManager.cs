using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneField nextSceneAsset;
    [SerializeField] private GameObject FinishUIPrefab;

    public void Finish()
    {
        FindObjectOfType<InputHandler>().DeactivateControls();
        FindObjectOfType<MainCamera>().SetCamera(MainCamera.CameraName.Finish);
        FindObjectOfType<CounterUI>().ShowResult();

        GameObject.Instantiate(FinishUIPrefab);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(nextSceneAsset, LoadSceneMode.Single);
    }
}
