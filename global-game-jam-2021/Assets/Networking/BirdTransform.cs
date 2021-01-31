using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class BirdTransform : Bolt.EntityBehaviour<IBirdState>
{
    public override void Attached()
    {
        state.SetTransforms(state.BirdTransform, transform);
    }
}
