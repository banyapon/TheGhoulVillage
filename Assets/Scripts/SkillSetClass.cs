using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetClass : MonoBehaviour
{
    public string skillChoose;
    void Start()
    {
        skillChoose = "easy";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Choose(string label){
        PlayerPrefs.SetString("level",label);
    }
}
