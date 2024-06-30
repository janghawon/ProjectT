using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotManager : MonoSingleton<TarotManager>
{
    [SerializeField] private TarotCardInfo[] _tarotContainer;
    public TarotCardInfo[] TarotContainer => _tarotContainer;

    private Dictionary<int, TarotCardInfo> _tarotCardDic = new();

    private void Awake()
    {
        foreach (var tarot in _tarotContainer)
        {
            if (_tarotCardDic.ContainsKey(tarot.id)) continue;

            _tarotCardDic.Add(tarot.id, tarot);
        }
    }

    public TarotCardInfo GetTarotInfoByID(int tarotID)
    {
        if(!_tarotCardDic.ContainsKey(tarotID))
        {
            Debug.LogError($"Error : Not Found Tarot Info By ID : {tarotID}");
            return null;
        }

        return _tarotCardDic[tarotID];
    }
}
