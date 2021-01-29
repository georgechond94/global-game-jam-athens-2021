using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class PlayerTransform : Bolt.EntityBehaviour<IFarmerState>
{
    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.FarmerTransform, transform);
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        CustomEvent.Trigger(gameObject, "SimulateOwner");
    }
}
