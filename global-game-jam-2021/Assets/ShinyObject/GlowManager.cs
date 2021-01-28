using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowManager : MonoBehaviour
{
    Material glow, nonGlow;
    bool isGlowing = true;

    public void ToggleGlow() 
    {
        gameObject.GetComponent<MeshRenderer>().material = isGlowing ? glow : nonGlow;
        isGlowing = !isGlowing;
    }
}
