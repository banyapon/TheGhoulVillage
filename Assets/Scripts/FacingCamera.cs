using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    public bool isPlayer = true;
    float screenWidth;
    float screenHeight;
    Vector3 screenCenter;
    private SpriteRenderer mySpriteRenderer;
    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        screenCenter = new Vector3(screenWidth / 2, screenHeight / 2, 0);
        if (isPlayer)
        {
            if (Input.mousePosition.x < screenCenter.x)
            {
                mySpriteRenderer.flipX = false;
            }
            else
            {
                mySpriteRenderer.flipX = true;
            }
        }
        else
        {
            if (this.gameObject.transform.position.x < screenCenter.x)
            {
                mySpriteRenderer.flipX = false;
            }
            else
            {
                mySpriteRenderer.flipX = true;
            }
        }


    }
}
