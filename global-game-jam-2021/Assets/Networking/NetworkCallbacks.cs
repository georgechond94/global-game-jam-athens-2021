
using UnityEngine;
using Bolt;
using Cinemachine;

public class NetworkCallbacks : GlobalEventListener
{
    public GameObject playerPrefab;
    public GameObject birdPrefab;
    public GameObject[] playerSpawnLocations;
    public GameObject[] birdSpawnLocations;
    public override void SceneLoadLocalDone(string scene)
    {
        GameObject prefab;
        GameObject spawn;
        if (BoltNetwork.IsServer)
        {
            prefab = playerPrefab;
            var spawnLoc1 = Random.Range(0, playerSpawnLocations.Length);
            spawn = playerSpawnLocations[spawnLoc1];
        }
        else
        {
            prefab = birdPrefab;
            var spawnLoc1 = Random.Range(0, birdSpawnLocations.Length);
            spawn = birdSpawnLocations[spawnLoc1];
        }
        GameObject newPlayer = BoltNetwork.Instantiate(prefab, spawn.transform.position, Quaternion.identity);
        var virtualCameras = Resources.FindObjectsOfTypeAll<CinemachineVirtualCamera>();
        foreach (var cinemachineVirtualCamera in virtualCameras)
        {
            cinemachineVirtualCamera.Follow = cinemachineVirtualCamera.LookAt = newPlayer.transform.Find("GFX/head");
        }

    }
}
