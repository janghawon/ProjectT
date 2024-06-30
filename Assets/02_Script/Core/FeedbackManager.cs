using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FeedbackObject
{

    public string key;
    public MMF_Player feedbackObject;

}

public class FeedbackManager : MonoSingleton<FeedbackManager>
{

    [SerializeField] private List<FeedbackObject> _feedbackObjects;

    public void PlayFeedback(string key)
    {

        var obj = _feedbackObjects.Find(x => x.key == key);

        if(obj != null)
        {

            obj?.feedbackObject.PlayFeedbacks();

        }

    }

}
