using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControll : MonoBehaviour
{
    public float z = 60f; // 화살표 z축 좌표
    public bool button = false;

    private void Start()
    {
        
    }


    void Update()
    {
       ArrowRotatation();
       // z += Time.deltaTime * -100;

    }

    void ArrowRotatation()
    {
        if (gameObject.transform.rotation.z <= 60f && !button  )
        {
            z += Time.deltaTime * -100;
            this.transform.rotation = Quaternion.Euler(0, 0, z);
        }
        else if (gameObject.transform.rotation.z >= -60f && button)
        {
            z += Time.deltaTime * 100;
            this.transform.rotation = Quaternion.Euler(0, 0, z);

        }

        if (gameObject.transform.rotation.z == -60f) button = true;
        if (gameObject.transform.rotation.z == 60f) button = false;


    }

}
