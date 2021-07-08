using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 15;
    private float upLimit = 1260;
    private float downLimit = 425;   
    private int direction = 1;
    private bool isStoped = false;
    public bool P_isStoped { get { return isStoped; } }

    public Vector2 BarPosition;
    public RectTransform BarTransform;

    // Update is called once per frame
    void Update()
    {
        if(isStoped == false)
            MoveBar();
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
