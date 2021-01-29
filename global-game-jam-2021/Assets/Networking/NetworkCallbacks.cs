
using UnityEngine;
using Bolt;
using Cinemachine;

public class NetworkCallbacks : GlobalEventListener
{
    public GameObject playerPrefab;
    public override void SceneLoadLocalDone(string scene)
    {
        var spawnPos = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));
        GameObject newPlayer = BoltNetwork.Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        var virtualCameras = Resources.FindObjectsOfTypeAll<CinemachineVirtualCamera>();
        foreach (var cinemachineVirtualCamera in virtualCameras)
        {
            cinemachineVirtualCamera.Follow = cinemachineVirtualCamera.LookAt = newPlayer.transform.Find("GFX/head");
        }
    }
}
