using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UI;

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

    [SerializeField] private SceneUIObject[] _screenElementGroup;

    private Dictionary<SceneType, SceneUIObject> _sceneUIDic = new ();
    private SceneUIObject _currentSceneUIObject;

    private void Start()
    {
        foreach (SceneUIObject su in _screenElementGroup)
        {
            _sceneUIDic.Add(su.UIType, su);
        }
    }

    public T GetSceneUI<T>() where T : SceneUIObject
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
            SceneUIObject suObject = Instantiate(_sceneUIDic[toChangeUIType], CanvasTrm);
            suObject.gameObject.name = _sceneUIDic[toChangeUIType].gameObject.name + "_[SceneUI]_";

            suObject.SceneUIStart();
            suObject.GenerateAllChildren();

            _currentSceneUIObject = suObject;
        }
    }
}
