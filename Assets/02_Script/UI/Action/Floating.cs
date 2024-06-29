using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    [SerializeField] private float _floatingValue;
    [SerializeField] private float _floatingSpeed;
    [SerializeField] private bool _useRandom;
    private float _randomValue;
    private Vector2 _normalPos;

    private void Awake()
    {
        _normalPos = transform.localPosition;
        _randomValue = Random.Range(0.8f, 5f);
    }

    private void FixedUpdate()
    {
        transform.localPosition +=
        new Vector3(0, Mathf.Sin(Time.fixedTime * _floatingSpeed * _randomValue) * _floatingValue);
    }
}
