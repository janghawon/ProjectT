using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Class: ToggleObject
* Author: ���Ͽ�
* Created: 2024�� 6�� 18�� ȭ����
* Description: ��� ��� ����
*/

namespace UIFunction
{
    public class ToggleObject : UIObject
    {
        [Header("ToggleSystem")]
        #region ToggleSystem
        private bool _isOnActivation;
        public bool Value => _isOnActivation;

        private UIObject _toggleHandleObject;
        private UIObject ToggleHandle => _toggleHandleObject;

        private bool _isInBehaviour = false;
        #endregion

        [Header("ToggleSettingValue")]
        #region ToggleSettingValue
        [SerializeField] private float _toggleTime;
        public float ToggleTime
        {
            get => _toggleTime;
            set
            {
                _toggleTime = value;
            }
        }

        [SerializeField] private RectTransform _handleSideArea;

        [SerializeField] private Color _unActiveHandleColor;
        public Color UnActiveHandleColor
        {
            get => _unActiveHandleColor;
            set
            {
                _unActiveHandleColor = value;
            }
        }

        [SerializeField] private Color _unActiveToggleBaseColor;
        public Color UnActiveToggleBaseColor
        {
            get => _unActiveToggleBaseColor;
            set
            {
                _unActiveToggleBaseColor = value;
            }
        }

        [SerializeField] private Color _activeHandleColor;
        public Color ActiveHandleColor
        {
            get => _activeHandleColor;
            set
            {
                _activeHandleColor = value;
            }
        }

        [SerializeField] private Color _activeToggleBaseColor;
        public Color ActiveToggleBaseColor
        {
            get => _activeToggleBaseColor;
            set
            {
                _activeToggleBaseColor = value;
            }
        }
        #endregion

        private void Awake()
        {
            _toggleHandleObject = GetComponent<UIObject>();
            _toggleHandleObject.OnClickEvent += HandleToggleHandleClick;

            ToggleActivation(Value);
        }

        private void HandleToggleHandleClick()
        {
            if (_isInBehaviour) return;

            _isOnActivation = !_isOnActivation;
            ToggleActivationBehaviour(_isOnActivation);
        }

        private void ToggleActivation(bool value)
        {
            (int, Color, Color) toggleValueGroup = GetTogglingValue(value);

            _toggleHandleObject.transform.localPosition = 
            new Vector2((_handleSideArea.sizeDelta.x * 0.5f) * toggleValueGroup.Item1, _toggleHandleObject.transform.localPosition.y);

            _toggleHandleObject.Visual.color = toggleValueGroup.Item2;
            Visual.color = toggleValueGroup.Item3;
        }

        private void ToggleActivationBehaviour(bool value)
        {
            (int, Color, Color) toggleValueGroup = GetTogglingValue(value);

        }

        private (int, Color, Color) GetTogglingValue(bool value)
        {
            int temp = value ? 1 : -1;
            Color handleColor = value ? ActiveHandleColor : UnActiveHandleColor;
            Color toggleBaseColor = value ? ActiveToggleBaseColor : UnActiveToggleBaseColor;

            return (temp, handleColor, toggleBaseColor);
        }
    }
}