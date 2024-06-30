using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugStart : MonoBehaviour
{
    
    public void StartPass()
    {

        GameManager.Instance.LoadScene("Game");

    }

}
