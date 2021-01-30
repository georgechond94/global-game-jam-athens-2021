
using UnityEngine;
using Bolt;
using Cinemachine;

public class NetworkCallbacks : GlobalEventListener
{
    public GameObject playerPrefab;
    public GameObject throwablePrefab;
    public override void SceneLoadLocalDone(string scene)
    {
        var spawnPos1 = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));
        var spawnPos2 = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));
        GameObject newPlayer = BoltNetwork.Instantiate(playerPrefab, spawnPos1, Quaternion.identity);
        BoltNetwork.Instantiate(throwablePrefab, spawnPos2, Quaternion.identity);
        var virtualCameras = Resources.FindObjectsOfTypeAll<CinemachineVirtualCamera>();
        foreach (var cinemachineVirtualCamera in virtualCameras)
        {
            cinemachineVirtualCamera.Follow = cinemachineVirtualCamera.LookAt = newPlayer.transform.Find("GFX/head");
        }
    }
}
