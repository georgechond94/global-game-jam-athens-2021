using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bolt;
using Cinemachine;
using UnityEngine;

public class PlayerFocusing : Bolt.EntityBehaviour<IFarmerState>
{
    public RectTransform ReticleTransform;
    private CinemachineVirtualCamera mainCamera;
    private CinemachineVirtualCamera aimCamera;

    private CancellationTokenSource cancellationTokenSource;
    // Start is called before the first frame update
    public override void Attached()
    {
        if (!entity.IsOwner)
        {
            return;
        }
        var virtualCameras = Resources.FindObjectsOfTypeAll<CinemachineVirtualCamera>();
        mainCamera = virtualCameras.FirstOrDefault(vc => vc.tag == "VirtualCamera");
        aimCamera = virtualCameras.FirstOrDefault(vc => vc.tag == "VirtualCameraFocus");
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        if (Input.GetButton("Fire2") && !aimCamera.gameObject.activeInHierarchy)
        {
            mainCamera.gameObject.SetActive(false);
            aimCamera.gameObject.SetActive(true);
            var enemy = FindClosestEnemy(tag);
            if (enemy != null)
            {
                Debug.LogError("asd");
                aimCamera.LookAt = enemy.transform;
            }
            SetActiveAsync(ReticleTransform.gameObject);


        }
        else if (!Input.GetButton("Fire2") && !mainCamera.gameObject.activeInHierarchy)
        {
            mainCamera.gameObject.SetActive(true);
            aimCamera.gameObject.SetActive(false);
            ReticleTransform.gameObject.SetActive(false);
            cancellationTokenSource.Cancel();

        }

        if (aimCamera.gameObject.activeInHierarchy)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(
                transform.rotation.x,
                aimCamera.transform.rotation.y,
                transform.rotation.z,
                aimCamera.transform.rotation.w), 3f * BoltNetwork.FrameDeltaTime);
        }
    }

    public GameObject FindClosestEnemy(string tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
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
