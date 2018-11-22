using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ModeChangerComponent : AppElement {

    public abstract void SetPlayMode();


    public abstract void SetBuildMode();


    public abstract bool IsBuildabel();

}
