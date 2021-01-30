using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableTransform : Bolt.EntityBehaviour<IThrowableState>
{
    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.ThrowableTransform, transform);
    }


}
