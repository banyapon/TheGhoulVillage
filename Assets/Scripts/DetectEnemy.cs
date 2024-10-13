using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    public TextMesh textMesh;
    ResultJosephus resultJosephus;
    public GameObject hiddenHead;
    void Start(){
        hiddenHead.SetActive(false);
        if (resultJosephus == null)
        {
            GameObject _temp = GameObject.FindGameObjectWithTag("GameController") as GameObject;
            resultJosephus = _temp.GetComponent<ResultJosephus>();
        }
        StartCoroutine(DisplayHidden());
    }

    IEnumerator DisplayHidden()
    {
        yield return new WaitForSeconds(0.5f);
        string itsme =  resultJosephus.choose;
        if(this.gameObject.name == itsme){
            textMesh.color = Color.red;
            hiddenHead.SetActive(true);
        }
    }


}
