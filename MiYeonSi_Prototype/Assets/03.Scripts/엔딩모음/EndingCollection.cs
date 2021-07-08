using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCollection : MonoBehaviour
{
    public GameObject[] EndingCollections; //  0.찬우  1. 은지   2. 수진   3. 민석   4. 하경

    void Update()
    {       
    }

    public void CollectEnding()
    {
        // 찬우 엔딩 해금
        if (SaveData.P_instance.EndingCollection.ContainsKey("Solo_Ending"))
            EndingCollections[0].SetActive(true);
        // 은지 엔딩 해금
        if (SaveData.P_instance.EndingCollection.ContainsKey("Enji_Ending"))
            EndingCollections[1].SetActive(true);
        // 수진 엔딩 해금
        if (SaveData.P_instance.EndingCollection.ContainsKey("Sujin_Ending"))
            EndingCollections[2].SetActive(true);
        // 민석 엔딩 해금
        if (SaveData.P_instance.EndingCollection.ContainsKey("Minseok_Ending"))
            EndingCollections[3].SetActive(true);
        // 하경 엔딩 해금
        if (SaveData.P_instance.EndingCollection.ContainsKey("Hakyung_Ending"))
            EndingCollections[4].SetActive(true);
    }
}
