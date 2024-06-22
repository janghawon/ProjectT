using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public abstract class PoolableMono : ExtensionMono
{
    public PoolingType poolingType;
    public abstract void Init();
}
