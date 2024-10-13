using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetScore : MonoBehaviour
{
    public int surviveDay = 0;
    public int day = 0;
    private const string HIGH_SCORE_KEY = "HighScore";
    public Text surviveDate;
    void Start()
    {
        surviveDay = PlayerPrefs.GetInt(HIGH_SCORE_KEY);
        surviveDate.text = ""+surviveDay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
