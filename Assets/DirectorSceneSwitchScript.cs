using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorSceneSwitchScript : MonoBehaviour
{
    public PlayableDirector director;

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
