
using UnityEngine;
using Bolt;
using Cinemachine;

public class NetworkCallbacks : GlobalEventListener
{
    public CinemachineVirtualCamera followCamera;
    public GameObject playerPrefab;
    public override void SceneLoadLocalDone(string scene)
    {
        var spawnPos = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));
        GameObject newPlayer = BoltNetwork.Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        followCamera.Follow = followCamera.LookAt = newPlayer.transform.Find("GFX/head");
    }
}
