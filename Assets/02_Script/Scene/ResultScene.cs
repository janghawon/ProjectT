using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScene : MonoBehaviour
{

    private IEnumerator Start()
    {

        yield return null;

        GameManager.Instance.UnLoadScene("Game");
        Debug.Log(PlayerPrefs.GetInt("DIE_PLAYER", -1));

    }

}
