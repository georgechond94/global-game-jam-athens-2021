
using UnityEngine;
using Bolt;
using Cinemachine;

public class NetworkCallbacks : GlobalEventListener
{
    public GameObject playerPrefab;
    public GameObject birdPrefab;
    public GameObject[] playerSpawnLocations;
    public override void SceneLoadLocalDone(string scene)
    {
        GameObject prefab;
        if (BoltNetwork.IsServer)
        {
            prefab = playerPrefab;
        }
        else
        {
            prefab = birdPrefab;
        }
        var spawnLoc = Random.Range(0, playerSpawnLocations.Length);
        GameObject newPlayer = BoltNetwork.Instantiate(prefab, playerSpawnLocations[spawnLoc].transform.position, Quaternion.identity);
        var virtualCameras = Resources.FindObjectsOfTypeAll<CinemachineVirtualCamera>();
        foreach (var cinemachineVirtualCamera in virtualCameras)
        {
            cinemachineVirtualCamera.Follow = cinemachineVirtualCamera.LookAt = newPlayer.transform.Find("GFX/head");
        }

    }
}
