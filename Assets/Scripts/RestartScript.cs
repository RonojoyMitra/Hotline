using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    [SerializeField] HealthComponent playerHealthComp;
    [SerializeField] GameObject MusicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(MusicPlayer)
        {
            DontDestroyOnLoad(MusicPlayer);
        }
        
        if(!playerHealthComp)
        {
            Debug.Log("Restart script could not find player healthcomp");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(playerHealthComp.IsDead)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
