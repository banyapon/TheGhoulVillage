using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVase : MonoBehaviour
{
    public string sortingLayerName = string.Empty; //initialization before the methods
    public int orderInLayer = 0;
    public Renderer MyRenderer;
    public Transform textMeshTransform;
    public TextMesh textMesh;
    public float scaleFactor = 1.5f;
    private Vector3 originalScale;

    void Start()
    {

        SetSortingLayer();
        textMesh.text = "" + this.gameObject.name;
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
    }

}
