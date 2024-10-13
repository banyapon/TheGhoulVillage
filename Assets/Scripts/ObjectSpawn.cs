using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public string sortingLayerName = string.Empty; //initialization before the methods
    public int orderInLayer = 0;
    public Renderer MyRenderer;
    public Transform textMeshTransform;
    public TextMesh textMesh;
    public float scaleFactor = 1.5f;
    private Vector3 originalScale;
    JosephusCircle josephusCircle;
    public GameObject textLabel;
    LoadSceneManager loadSceneManager;

    public string chooseVas;

    void Start()
    {
        if (josephusCircle == null)
        {
            GameObject _temp = GameObject.FindGameObjectWithTag("GameController") as GameObject;
            josephusCircle = _temp.GetComponent<JosephusCircle>();
        }
        if (loadSceneManager == null)
        {
            GameObject _loadScene = GameObject.FindGameObjectWithTag("GameController") as GameObject;
            loadSceneManager = _loadScene.GetComponent<LoadSceneManager>();
        }
        SetSortingLayer();
        textMesh.text = "" + this.gameObject.name;
        textLabel.SetActive(false);

        originalScale = transform.localScale;
    }

    void SetSortingLayer()
    {
        if (sortingLayerName != string.Empty)
        {
            MyRenderer.sortingLayerName = sortingLayerName;
            MyRenderer.sortingOrder = orderInLayer;
        }
    }
    void Update()
    {
        textMeshTransform.rotation = Quaternion.LookRotation(textMeshTransform.position - Camera.main.transform.position);
        if(josephusCircle.vaseOn){
            textLabel.SetActive(true);
        }
    }
    void OnMouseOver()
    {
        if(josephusCircle.vaseOn){
            transform.localScale = originalScale * scaleFactor;
        }
    }

    void OnMouseDown()
    {
        if(josephusCircle.vaseOn){
            chooseVas = this.gameObject.name;
            PlayerPrefs.SetString("choose", this.gameObject.name);
        }
    }

    void OnMouseExit()
    {
        if(josephusCircle.vaseOn){
            transform.localScale = originalScale;
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && this.gameObject.name == chooseVas)
        {
            if(josephusCircle.vaseOn){
                loadSceneManager.LoadScene("Result");
            }
        }
    }


}
