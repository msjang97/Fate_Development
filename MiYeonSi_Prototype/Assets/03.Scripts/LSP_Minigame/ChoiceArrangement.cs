using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceArrangement : MonoBehaviour
{
    public GameObject best; 
    public GameObject good; 
    public GameObject worst;

    public float bestMoveX;
    public float goodMoveX;
    public float worstMoveX;

    public float[] xxx = new float[2];
    // best - (325) (540)
    // good - (330) (750)
    private void Awake()
    {
        xxx[0] = -356f; ;
        xxx[1] = 242f;
    }
    private void Start()
    {
        MovePosition();
    }


    void MovePosition()
    {
       
        bestMoveX = Random.Range(-215, 215);
        goodMoveX = Random.Range(0, 2);
        if (goodMoveX == 0)
            worstMoveX = 1;
        else
            worstMoveX = 0;
        

        best.transform.localPosition = new Vector3(bestMoveX, 465f, 0f);
        good.transform.localPosition = new Vector3(xxx[(int)goodMoveX], -401f, 0f);
        worst.transform.localPosition = new Vector3(xxx[(int)worstMoveX], -531f, 0f);

    }

}
