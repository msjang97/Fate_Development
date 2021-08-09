using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceArrangement : MonoBehaviour
{
    public GameObject best; //회색
    public GameObject good; //노란색

    public float bestMoveX;
    public float goodMoveX;
    // best - (325) (540)
    // good - (330) (750)
    private void Start()
    {
        MovePosition();
    }


    void MovePosition()
    {
        bestMoveX = Random.Range(-215, 215);
        goodMoveX = Random.Range(-210, 210);

        best.transform.localPosition = new Vector3(bestMoveX, 465f, 0f);
        good.transform.localPosition = new Vector3(goodMoveX, -425f, 0f);

    }

}
