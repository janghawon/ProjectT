using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    [SerializeField] private Transform _alcoholSpawnTrm;
    [SerializeField] private GameObject _alcoholPrefab;
    [SerializeField] private List<Transform> _itemSpawnTrms;

    private List<Transform> _ableTrms = new();

    private void Awake()
    {

        TurnManager.Instance.OnTurnChanged += HandleTurnChanged;

    }

    public bool SpawnItem(GameObject prefab)
    {

        if (_ableTrms.Count <= 0) return false; 

        var trm = _ableTrms[Random.Range(0, _ableTrms.Count)];
        var obj = Instantiate(prefab, trm.position, Quaternion.identity).GetComponent<ItemInstance>();

        obj.OnItemUsed += HandleItemUsed;

        _ableTrms.Remove(trm);

        return true;

        void HandleItemUsed()
        {

            _ableTrms.Add(trm);

        }

    }

    private void HandleTurnChanged(ulong oldId, ulong newId)
    {

        if (TurnManager.Instance.MyTurn)
        {

            SpawnAlcohol();

        }

    }

    private void SpawnAlcohol()
    {

        Instantiate(_alcoholPrefab, _alcoholSpawnTrm.position, Quaternion.identity);

    }

}
