using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    HealthComponent playerHealthComp;

    private static GameObject RestartManager;

    // Start is called before the first frame update
    void Start()
    {
        if(RestartManager == null)
        {
            RestartManager = this.gameObject;
            DontDestroyOnLoad(RestartManager);
        }
        else
        {
            if(RestartManager != this)
            {
                Destroy(this.gameObject);
            }
        }           
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>().IsDead)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
