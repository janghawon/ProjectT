using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class DrinkMoveData
{

    public Transform _notVisablePos;
    public Transform _visablePos;
    public GameObject _drink;

    public void Move(bool visable)
    {

        var pos = visable ? _visablePos : _notVisablePos;
        _drink.transform.DOMove(pos.position, 0.7f);

    }

}

public class Table : MonoBehaviour
{

    [SerializeField] private Transform _alcoholSpawnTrm;
    [SerializeField] private GameObject _alcoholPrefab;
    [SerializeField] private List<Transform> _itemSpawnTrms;
    [SerializeField] private List<DrinkMoveData> _drinkMoveDatas;
    [SerializeField] private bool _isEnemy;

    private List<Transform> _ableTrms = new();

    private void Awake()
    {

        _ableTrms = _itemSpawnTrms.ToList();

        if (_isEnemy) return;

        TurnManager.Instance.OnTurnChanged += HandleTurnChanged;

    }

    private void HandleTurnChanged(ulong oldId, ulong newId)
    {

        if (TurnManager.Instance.MyTurn)
        {

            foreach(var item in _drinkMoveDatas)
            {

                item.Move(true);

            }

        }
        else
        {

            foreach (var item in _drinkMoveDatas)
            {

                item.Move(false);

            }

        }

    }

    public bool SpawnItem(GameObject prefab, Guid guid, out ItemInstance ins)
    {

        ins = null;
        if (_ableTrms.Count <= 0) return false;

        var trm = _ableTrms[Random.Range(0, _ableTrms.Count)];
        var obj = Instantiate(prefab, trm.position, Quaternion.identity).GetComponent<ItemInstance>();

        ins = obj;
        obj.SetGuid(guid);
        obj.OnItemUsed += HandleItemUsed;

        _ableTrms.Remove(trm);

        return true;

        void HandleItemUsed()
        {

            _ableTrms.Add(trm);

        }

    }


}
