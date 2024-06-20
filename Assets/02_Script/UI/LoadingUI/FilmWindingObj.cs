using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Class: FilmWindingObj
* Author: 장하원
* Created: 2024년 6월 19일 수요일
* Description: 간단한 필름 이미지 와인딩 코드
*/

public class FilmWindingObj : MonoBehaviour
{
    [SerializeField] private Transform[] _filmTrmArr;

    [Header("Position Setting")]
    [SerializeField] private float _targetYValue;
    [SerializeField] private float _resetYValue;

    [Header("Moving Value")]
    [SerializeField] private float _speed;

    private void Update()
    {
        foreach(Transform t in _filmTrmArr)
        {
            Vector3 target = new Vector3(t.localPosition.x, _targetYValue);
            t.localPosition = Vector3.MoveTowards(t.localPosition, target, _speed);

            if (t.localPosition.y == _targetYValue)
            {
                t.localPosition = new Vector3(t.localPosition.x, _resetYValue);
            }
        }
    }
}
