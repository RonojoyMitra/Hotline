using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointManager : MonoBehaviour
{
    int ComboCount;
    public int totalScore;

    [SerializeField] GameObject playerChar;

    [SerializeField] float ComboTimeFrame = 2.0f;
    [SerializeField] GameObject pointTextPrefab;
    [SerializeField] GameObject comboTextPrefab;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] GameObject textCanvas;
    [SerializeField] GameObject floatingCanvasPrefab;

    [SerializeField] int EnemyPointsValue = 300;
    [SerializeField] float comboMultiplier = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
        if(!pointTextPrefab)
        {
            Debug.Log("Pop up text prefab was not assigned in PointManager object");
        }

        if(!totalScoreText)
        {
            Debug.Log("Total Score text was not assigned in PointManager object");
        }
    }

    public void AddPoints(Vector3 pointLocation)
    {
        // Create temporary canvas for floating point text, and create point text object       
        GameObject floatingCanvas = Instantiate(floatingCanvasPrefab);
        floatingCanvas.transform.position = Camera.main.transform.position - (Vector3.down);
        GameObject textObject = Instantiate(pointTextPrefab);

        // Set a bunch of transform parameters so that point text will appear on screen properly
        textObject.transform.SetParent(floatingCanvas.transform);
        textObject.transform.rotation = textObject.transform.parent.rotation;
        textObject.transform.localScale = textObject.transform.parent.localScale * 5;

        // TODO fix position y to prevent clipping
        textObject.transform.position = pointLocation;

        Destroy(floatingCanvas, 1.25f);

        totalScore += EnemyPointsValue;
        totalScoreText.text = totalScore.ToString() + "PTS";
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
        // Debug.Log(ComboCount);

        GameObject floatingCanvas = Instantiate(floatingCanvasPrefab);
        floatingCanvas.transform.position = Camera.main.transform.position - (Vector3.down);

        GameObject textObject = Instantiate(comboTextPrefab);
        textObject.transform.SetParent(floatingCanvas.transform);

        // Set a bunch of transform parameters so that text will appear on screen properly
        textObject.transform.SetParent(floatingCanvas.transform);
        textObject.transform.rotation = textObject.transform.parent.rotation;
        textObject.transform.localScale = textObject.transform.parent.localScale * 5;
        textObject.transform.position = playerChar.transform.position;

        Destroy(textObject, 1.25f);

        // calculate combo total
        int totalComboPoints = (int)(EnemyPointsValue * ComboCount * comboMultiplier);

        // assign combo count to text
        // TODO assign combo value to text as well
        textObject.GetComponent<TextMeshProUGUI>().text = ComboCount + "X COMBO";

        //Add to overall score
        totalScore += totalComboPoints;

        // Reset Combo Count
        ComboCount = 0;
    }
}
