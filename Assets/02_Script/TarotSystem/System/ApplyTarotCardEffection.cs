using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTarotCardEffection : MonoBehaviour
{
    [SerializeField] private TarotCardInfo[] _tarotInfoCantainer;
    private Dictionary<TarotCardInfo, Action> _tarotCardActionDic = new ();
    public Action OnTarotEffection { get; set; }

    private void Awake()
    {
        foreach(var t in _tarotInfoCantainer)
        {
            _tarotCardActionDic.Add(t, t.ApplyTarotEffect);
        }
    }

    public void ApplyTarotEffection(TarotCardInfo info)
    {
        if(!_tarotCardActionDic.ContainsKey(info))
        {
            Debug.LogError($"Error : Not Foundation Tarot Info : {info}");
            return;
        }

        _tarotCardActionDic[info].Invoke();
        OnTarotEffection?.Invoke();
    }
}
