using System.Collections;
using System.Collections.Generic;
using Bolt.Matchmaking;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : Bolt.EntityBehaviour<IFarmerState>
{
    //public ICinemachineCamera entityCamera;
    public Camera entityCamera;
    public CinemachineVirtualCamera followCamera;
    public Transform toFollow;
    public string userName;

    private int layerThreshold = 10;
    public override void Attached()
    {
        if (entity.IsOwner)
        {

            //followCamera.gameObject.layer = layerThreshold + BoltMatchmaking.CurrentSession.ConnectionsCurrent;
            //entityCamera.cullingMask = entityCamera.cullingMask | (1 << followCamera.gameObject.layer);
            //entityCamera.gameObject.SetActive(true);
        }
    }
}
