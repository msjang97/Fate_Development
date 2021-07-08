using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCollection : MonoBehaviour
{
    public GameObject[] EndingCollections; //  0.찬우  1. 은아   2. 준병   3. 민석   4. 아린

    void Update()
    {       
    }

    public void CollectEnding()
    {
        // 찬우 엔딩 해금
        if (SaveData.P_instance.EndingCollection.ContainsKey("Solo_Ending"))
            EndingCollections[0].SetActive(true);
        // 은아 엔딩 해금
        if (SaveData.P_instance.EndingCollection.ContainsKey("Euna_Ending"))
            EndingCollections[1].SetActive(true);
        // 준병 엔딩 해금
        if (SaveData.P_instance.EndingCollection.ContainsKey("Junbyeong_Ending"))
            EndingCollections[2].SetActive(true);
        // 민석 엔딩 해금
        if (SaveData.P_instance.EndingCollection.ContainsKey("Minseok_Ending"))
            EndingCollections[3].SetActive(true);
        // 아린 엔딩 해금
        if (SaveData.P_instance.EndingCollection.ContainsKey("Arin_Ending"))
            EndingCollections[4].SetActive(true);
    }
}
