using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    [SerializeField] private ItemInfo[] _infoContainer;
    private Dictionary<string, ItemInfo> _getItemInfoByNameDic = new ();

    private void Awake()
    {
        foreach (var item in _infoContainer)
        {
            _getItemInfoByNameDic.Add(item.itemName, item);
        }
    }

    public ItemInfo GetItem(string itemName)
    {
        return _getItemInfoByNameDic[itemName];
    }

    public ItemInfo GetRandomItem()
    {
        return _infoContainer[Random.Range(0, _infoContainer.Length)];
    }
}
