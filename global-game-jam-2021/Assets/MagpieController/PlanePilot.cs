using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class PlanePilot : MonoBehaviour
{
    public float speed = 20.0f;
    private Cinemachine.CinemachineBrain camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Resources.FindObjectsOfTypeAll<CinemachineBrain>().FirstOrDefault();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveCameTo = transform.position - transform.forward * 10.0f + Vector3.up * 5.0f;
        float bias = 0.96f;
        camera.transform.position = camera.transform.position * bias +
                                    moveCameTo * (1.0f - bias);
        camera.transform.LookAt(transform.position + transform.forward * 30.0f);

        transform.position += transform.forward * BoltNetwork.FrameDeltaTime * speed;

        speed -= transform.forward.y * BoltNetwork.FrameDeltaTime * 50.0f;
        speed = 5.0f;
        if (speed < 5.0f)
        {
            speed = 5.0f;
        } else if( speed > 150.0f)
        {

        }

        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));

        //float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);

        //if(terrainHeightWhereWeAre > transform.position.y)
        //{
        //    transform.position = new Vector3(transform.position.x,
        //                                    terrainHeightWhereWeAre,
        //                                    transform.position.z);
        //}
    }
}
