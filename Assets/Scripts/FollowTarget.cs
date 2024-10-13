using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public string sortingLayerName = string.Empty; //initialization before the methods
    public int orderInLayer = 0;
    public Renderer MyRenderer;
    private SpriteRenderer spriteRenderer;
    public Transform target; // GameObject ที่ต้องการติดตาม
    public float speed = 0f; // ความเร็วในการติดตาม
    public Vector3 offset = new Vector3(0, 0f, 0f);

    void Start(){
        SetSortingLayer();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Foreground";
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
        if (target != null)
        {
            /*
            // คำนวณตำแหน่งปลายทางโดยรวมรวม offset เข้าไป
            Vector3 desiredPosition = target.position + offset;

            // คำนวณทิศทางไปยังตำแหน่งปลายทาง
            Vector3 direction = desiredPosition - transform.position;

            // เคลื่อนที่ไปยังตำแหน่งปลายทาง
            transform.position += direction.normalized * speed * Time.deltaTime;
            */
            transform.position = target.position+ offset;
        }
    }
}
