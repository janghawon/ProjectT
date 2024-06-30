using System;
using UnityEngine;

public abstract class ItemInstance : MonoBehaviour
{

    [field: SerializeField] public ItemInfo Info { get; protected set; }

    public event Action OnItemUsed;
    public Guid GUID { get; protected set; }

    protected virtual void OnMouseOver()
    {
        //나중에 아웃라인
    }

    protected abstract void UseItem();
    protected abstract void UseLinkItem();

    protected virtual void OnMouseDown()
    {

        if (TurnManager.Instance.MyTurn && !GamePlayManager.Instance.IsUsingStore)
        {

            GamePlayManager.Instance.LinkUseItemServerRPC(GUID.ToString(), GamePlayManager.Instance.EnemyClientId);
            UseItem();
            OnItemUsed?.Invoke();

        }

    }

    public void SetGuid(Guid guid)
    {

        GUID = guid;

    }

    public void UseLink()
    {
        UseLinkItem();
        OnItemUsed?.Invoke();
    }
}
