using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Cam : MonoBehaviour
{

    Vector2 mouseLook;
    public float sensitivity;
    GameObject bird;

    // Start is called before the first frame update
    void Start()
    {
        bird = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        Vector2 look = new Vector2(horizontal, vertical);
        mouseLook += look * sensitivity;

        mouseLook.y = Mathf.Clamp(mouseLook.y, -80f, 80);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        bird.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, bird.transform.up);
    }
}
