using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 20;
    private float upLimit = 1680;
    private float downLimit = 280;   
    private int direction = 1;
    private bool isStoped = false;
    public bool P_isStoped { get { return isStoped; } }

    public Vector2 BarPosition;
    public RectTransform BarTransform;
    public GameObject manual;

    // Update is called once per frame
    void Update()
    {
        if( manual.activeSelf == false)
        {
            if (isStoped == false)            
                MoveBar();          
        }     
       // else       
         //   StopBar();       
    }

    private void MoveBar()
    {
        if (transform.position.y < downLimit)
            direction = 1;
        else if (transform.position.y > upLimit)
            direction = -1;

        BarTransform.Translate(Vector2.up * moveSpeed * Time.deltaTime * direction*100);       
    }

    public void StopBar()
    {
        isStoped = true;
        BarPosition = BarTransform.position;
        Choice.lovecheck = true;
    }

   
}
