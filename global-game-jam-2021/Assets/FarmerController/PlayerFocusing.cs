using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bolt;
using Cinemachine;
using UnityEngine;

public class PlayerFocusing : MonoBehaviour
{
    public RectTransform ReticleTransform;
    private CinemachineVirtualCamera mainCamera;
    private CinemachineVirtualCamera aimCamera;

    private CancellationTokenSource cancellationTokenSource;
    // Start is called before the first frame update
    void Start()
    {
        var virtualCameras = Resources.FindObjectsOfTypeAll<CinemachineVirtualCamera>();
        mainCamera = virtualCameras.FirstOrDefault(vc => vc.tag == "VirtualCamera");
        aimCamera = virtualCameras.FirstOrDefault(vc => vc.tag == "VirtualCameraFocus");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2") && !aimCamera.gameObject.activeInHierarchy)
        {
            mainCamera.gameObject.SetActive(false);
            aimCamera.gameObject.SetActive(true);
            SetActiveAsync(ReticleTransform.gameObject);


        }
        else if (!Input.GetButton("Fire2") && !mainCamera.gameObject.activeInHierarchy)
        {
            mainCamera.gameObject.SetActive(true);
            aimCamera.gameObject.SetActive(false);
            ReticleTransform.gameObject.SetActive(false);
            cancellationTokenSource.Cancel();

        }
    }

    private async void SetActiveAsync(GameObject gameObject)
    {
        cancellationTokenSource = new CancellationTokenSource();
        await Task.Delay(250).ContinueWith(_ =>
        {
            gameObject.SetActive(true);
        }, cancellationTokenSource.Token, 
            TaskContinuationOptions.None, 
            TaskScheduler.FromCurrentSynchronizationContext());
    }
}
