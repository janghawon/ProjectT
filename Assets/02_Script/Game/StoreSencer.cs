using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSencer : MonoBehaviour
{

    private void OnMouseDown()
    {

        if (TurnManager.Instance.MyTurn)
        {

            Debug.Log("������ ����");

        }

    }

}
