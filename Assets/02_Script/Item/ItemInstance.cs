using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public abstract class ItemInstance : MonoBehaviour
{

    [field: SerializeField] public ItemInfo Info { get; protected set; }

    public event Action OnItemUsed;

    protected virtual void OnMouseOver()
    {
        //���߿� �ƿ�����
    }

    protected abstract void UseItem();

    protected virtual void OnMouseDown()
    { 

        if (TurnManager.Instance.MyTurn)
        {

            UseItem();
            OnItemUsed?.Invoke();

        }

    }

}
