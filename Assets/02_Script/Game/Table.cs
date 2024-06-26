using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Table : MonoBehaviour
{

    [SerializeField] private Transform _alcoholSpawnTrm;
    [SerializeField] private GameObject _alcoholPrefab;
    [SerializeField] private List<Transform> _itemSpawnTrms;

    private List<Transform> _ableTrms = new();

    private void Awake()
    {

        _ableTrms = _itemSpawnTrms.ToList();
        SpawnAlcohol();

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

    private void SpawnAlcohol()
    {

        Instantiate(_alcoholPrefab, _alcoholSpawnTrm.position, Quaternion.identity);

    }

}
