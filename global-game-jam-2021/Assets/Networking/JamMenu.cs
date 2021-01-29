using System;
using UnityEngine;
using Bolt;
using Bolt.Matchmaking;
using UdpKit;

public class JamMenu : GlobalEventListener
{      
    //called from new game button
    public void StartServer()
    {
        BoltLauncher.StartServer();
    }
    
    public override void BoltStartDone()
    {
        BoltMatchmaking.CreateSession(sessionID: "test", sceneToLoad: "MagpiesGame");
    }

    //called from join game button
    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;
            
            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }
        }
    }
}
