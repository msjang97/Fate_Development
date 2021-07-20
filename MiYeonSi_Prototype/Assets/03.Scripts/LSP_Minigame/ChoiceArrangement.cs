using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceArrangement : MonoBehaviour
{
    public GameObject best;
    public GameObject good;

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

        best.transform.localPosition = new Vector3(bestMoveX, 215f, 0f);
        good.transform.localPosition = new Vector3(goodMoveX, -570f, 0f);

    }

}
