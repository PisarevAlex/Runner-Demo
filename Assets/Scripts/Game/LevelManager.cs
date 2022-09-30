using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SceneField nextSceneAsset;
    [SerializeField] private GameObject FinishUIPrefab;
    [SerializeField] private GameObject StartUIInstance;

    [SerializeField] private List<GameObject> inGameActiveObjects;

    public void StartLevel()
    {
        FindObjectOfType<CrowdController>().Run();
        FindObjectOfType<MainCamera>().SetCamera(MainCamera.CameraName.Play);
        Destroy(StartUIInstance);

        foreach (GameObject obj in inGameActiveObjects)
        {
            obj.SetActive(true);
        }
    }

    public void Finish()
    {
        FindObjectOfType<InputHandler>().DeactivatePlayerControls();
        FindObjectOfType<MainCamera>().SetCamera(MainCamera.CameraName.Finish);
        FindObjectOfType<CounterUI>().ShowResult();

        GameObject.Instantiate(FinishUIPrefab);

        foreach (GameObject obj in inGameActiveObjects)
        {
            obj.SetActive(false);
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync(nextSceneAsset, LoadSceneMode.Single);
    }
}
