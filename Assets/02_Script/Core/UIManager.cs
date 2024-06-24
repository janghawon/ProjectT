using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIFunction;

/*
* Class: UIManager
* Author: 厘窍盔
* Created: 2024斥 6岿 13老 格夸老
* Description: UI 积己 棺 包府
*/

public class UIManager : MonoSingleton<UIManager>
{
    private RectTransform _canvasTrm;
    public RectTransform CanvasTrm
    {
        get
        {
            if(_canvasTrm == null )
            {
                _canvasTrm = GameObject.FindAnyObjectByType<Canvas>().transform as RectTransform;
            }

            return _canvasTrm;
        }
    }

    [SerializeField] private SceneType _startSceneType;
    [SerializeField] private SceneUIContent[] _screenElementGroup;

    private Dictionary<SceneType, SceneUIContent> _sceneUIDic = new ();
    private SceneUIContent _currentSceneUIObject;
    public SceneUIContent CurrentSceneUiObject => _currentSceneUIObject;

    private void Start()
    {
        foreach (SceneUIContent su in _screenElementGroup)
        {
            _sceneUIDic.Add(su.UIType, su);
        }

        ChangeSceneUIOnChangeScene(_startSceneType);
    }

    #region GetUIKewordMask

    public UIKeyword GetUIKewordMask(UIKeyword keword1, UIKeyword keword2)
    {
        return (keword1 |= keword2);
    }

    public UIKeyword GetUIKewordMask(UIKeyword keword1, UIKeyword keword2, UIKeyword keword3)
    {
        return (keword1 |= keword2 |= keword3);
    }

    public UIKeyword GetUIKewordMask(UIKeyword keword1, UIKeyword keword2, UIKeyword keword3, UIKeyword keword4)
    {
        return (keword1 |= keword2 |= keword3 |= keword4);
    }

    public UIKeyword GetUIKewordMask(UIKeyword keword1, UIKeyword keword2, UIKeyword keword3, UIKeyword keword4, UIKeyword keword5)
    {
        return (keword1 |= keword2 |= keword3 |= keword4 |= keword5);
    }

    #endregion

    public T GetSceneUIContent<T>() where T : SceneUIContent
    {
        return (T)FindFirstObjectByType(typeof(T));
    }

    public void ChangeSceneUIOnChangeScene(SceneType toChangeUIType)
    {
        if (_currentSceneUIObject != null)
        {
            _currentSceneUIObject.SceneUIEnd();
            Destroy(_currentSceneUIObject.gameObject);
        }

        if (_sceneUIDic.ContainsKey(toChangeUIType))
        {
            SceneUIContent suObject = Instantiate(_sceneUIDic[toChangeUIType], CanvasTrm);
            suObject.gameObject.name = _sceneUIDic[toChangeUIType].gameObject.name + "_[SceneUI]_";

            suObject.GenerateOnUIObject();
            suObject.SceneUIStart();

            _currentSceneUIObject = suObject;
        }
    }
}
