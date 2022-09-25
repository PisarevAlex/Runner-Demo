using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCamera : MonoBehaviour
{
    public enum CameraName
    {
        Start = 0,
        Play = 1,
        Finish = 2,
    }

    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras;
    [SerializeField] private CameraName currentCameraName;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        foreach (CinemachineVirtualCamera camera in virtualCameras)
        {
            camera.gameObject.SetActive(false);
        }

        currentCameraName = CameraName.Finish;
        SetCamera(CameraName.Start);
    }

    public void SetCamera(CameraName cameraName)
    {
        if (cameraName == currentCameraName) return;

        foreach (CinemachineVirtualCamera camera in virtualCameras)
        {
            if ((int)cameraName == virtualCameras.IndexOf(camera))
            {
                camera.gameObject.SetActive(true);
                virtualCameras[(int)currentCameraName].gameObject.SetActive(false);
                currentCameraName = cameraName;
                break;
            }
        }
    }
}
