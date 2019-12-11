using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("endscene");
        Debug.Log("end");
        if (gameObject.tag == "end")
        {
            SceneManager.LoadScene("endscene");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("end");
        SceneManager.LoadScene("endscene");
    }
}
