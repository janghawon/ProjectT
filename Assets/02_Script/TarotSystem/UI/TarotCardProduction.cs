using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFunction;
using UnityEngine;
using Random = UnityEngine.Random;

/*
* Class: TarotCardProduction
* Author: 장하원
* Created: 2024년 6월 20일 목요일
* Description: 타로 카드 연출 프린터
*/

public class TarotCardProduction : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float _idlingValue;
    [SerializeField] private float _hoveringValue;
    [SerializeField] private float _lerpValue;

    [Header("Printing Value")]
    private bool _canProduction = false;
    private TarotCard[] _toProductionTarotArr;
    private int[] _randValueArr;

    public void AppearBackFace(Transform backFaceTrm)
    {
        backFaceTrm.localScale = Vector3.one * 0.8f;
        backFaceTrm.rotation = Quaternion.Euler(13, 4, 15);

        Sequence seq = DOTween.Sequence();
        seq.Append(backFaceTrm.DOScale(Vector3.one * 1.35f, 0.3f));
        seq.Join(backFaceTrm.DOLocalRotateQuaternion(Quaternion.identity, 0.4f).SetEase(Ease.OutBack));
        seq.Append(backFaceTrm.DOScale(Vector3.one, 0.3f));
    }
    public void DisAppearBackFace(Transform backFaceTrm)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(backFaceTrm.DOScale(Vector3.one * 1.35f, 0.15f));
        seq.Join(backFaceTrm.DOLocalRotateQuaternion(Quaternion.Euler(0, 90, 30), 0.15f));
        seq.AppendCallback(() => Destroy(backFaceTrm.gameObject));
    }
    public void AppearTartoCard(Transform tarotTrm)
    {
        tarotTrm.localScale = Vector3.one * 1.35f;
        tarotTrm.rotation = Quaternion.Euler(0, 90, 30);

        Sequence seq = DOTween.Sequence();
        seq.Append(tarotTrm.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutBack));
        seq.Join(tarotTrm.DOLocalRotateQuaternion(Quaternion.identity, 0.3f).SetEase(Ease.OutBack));
    }
    public void StartProduction(TarotCard[] tarotArr)
    {
        _toProductionTarotArr = tarotArr;

        foreach (var tar in tarotArr)
        {
            tar.OnHoverEvent += TarotHoverAction;
            tar.OnDesecendEvent += TarotDescendAction;
        }

        _randValueArr = new int[tarotArr.Length];
        for(int i = 0; i < _randValueArr.Length; i++)
        {
            _randValueArr[i] = Random.Range(-3, 3);
        }

        _canProduction = true;
    }

    public void SelectTarotProduction(TarotCard target, TarotCard[] tarotArr)
    {
        _canProduction = false;

        foreach (var tar in tarotArr)
        {
            tar.CanSelect = false;
            tar.SetLabelText(string.Empty, string.Empty);

            if(tar == target)
            {
                StartSelectTarotProduc(target.transform);
                continue;
            }
            StartUnSelectTarotProduc(tar.transform);
        }
    }

    private void StartSelectTarotProduc(Transform transform)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1.3f, 0.2f));
        seq.Join(transform.DOLocalRotateQuaternion(Quaternion.identity, 0.1f));
        seq.AppendInterval(0.5f);
        seq.Append(transform.DOLocalMoveY(50, 0.2f));
        seq.Append(transform.DOLocalMoveY(-950, 0.4f).SetEase(Ease.OutQuart));
        seq.AppendCallback(() =>
        {

            GamePlayManager.Instance.CheckSelectTarot();

        });
    }

    private void StartUnSelectTarotProduc(Transform transform)
    {
        transform.DOLocalMoveY(-900, 0.4f).SetEase(Ease.OutBack);
    }

    private void TarotDescendAction(UIObject obj)
    {
        TarotCard tc = obj as TarotCard;

        tc.VisualTrm.DOKill();
        tc.VisualTrm.DOScale(Vector3.one, 0.2f);
    }

    private void TarotHoverAction(UIObject obj)
    {
        TarotCard tc = obj as TarotCard;
        tc.VisualTrm.DOKill();
        tc.VisualTrm.DOScale(Vector3.one * 1.05f, 0.2f).SetEase(Ease.OutBack);
    }

    private void FixedUpdate()
    {
        if (!_canProduction) return;

        for(int i = 0; i < _toProductionTarotArr.Length; i++)
        {
            if (_toProductionTarotArr[i].OnPointerThisCard)
            {
                Vector2 mousePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.CanvasTrm, Input.mousePosition, Camera.main, out mousePos);

                Vector3 offset = _toProductionTarotArr[i].transform.localPosition - new Vector3(mousePos.x, mousePos.y);

                float tiltX = -offset.y;
                float tiltY = offset.x;

                Transform trm = _toProductionTarotArr[i].VisualTrm;

                trm.localEulerAngles = new Vector3(tiltX, tiltY) * _hoveringValue;
            }
            else
            {
                float sinX = Mathf.Sin((Time.fixedTime + _randValueArr[i]));
                float cosX = Mathf.Cos((Time.fixedTime + _randValueArr[i]));

                Transform trm = _toProductionTarotArr[i].VisualTrm;

                float lerpX = Mathf.LerpAngle(trm.localPosition.x, sinX, Time.fixedDeltaTime * _lerpValue);
                float lerpY = Mathf.LerpAngle(trm.localPosition.y, cosX, Time.fixedDeltaTime * _lerpValue);

                trm.localEulerAngles = new Vector3(lerpX, lerpY, 0) * _idlingValue;
            }
        }
    }
}
