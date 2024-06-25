using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePassManager : NetworkMonoSingleton<GamePassManager>
{

    public void StartGamePass()
    {

        StartCoroutine(StartPass());

    }

    private IEnumerator StartPass()
    {

        yield return null;

        var array = FindObjectsOfType<MonoBehaviour>().OfType<INetworkInitable>();

        foreach(var obj in array)
        {

            obj.Init();

        }

    }
}
