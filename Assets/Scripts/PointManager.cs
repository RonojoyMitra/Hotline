using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    int ComboCount;
    public int totalScore;
    [SerializeField] float ComboTimeFrame = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddComboCount()
    {
        ComboCount++;

        //Cancel method call to reset time whenever adding combo
        CancelInvoke("CountCombo");

        //count combo points if more combo count is more than 1
        if(ComboCount > 1)
        {
            //Call method to count up combo points after given time frame
            Invoke("CountCombo", ComboTimeFrame);
        }             
    }

    void CountCombo()
    {
        Debug.Log(ComboCount);
        //Show combo text fx

        //Add to overall score

        ComboCount = 0;
    }
}
