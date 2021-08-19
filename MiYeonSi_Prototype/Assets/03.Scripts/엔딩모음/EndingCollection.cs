using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCollection : MonoBehaviour
{
    public GameObject[] EndingCollections; //13. 은지   14. 준병   15. 민석   16. 아린 17. 솔로

    void Update()
    {       
    }

    public void CollectEnding()
    {
        //나머지 일러스트는 일러스트 이름으로 넘어와서 판단

        // 프롤로그1
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("P_1"))
            EndingCollections[0].SetActive(true);
        // 프롤로그2
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("P_2"))
            EndingCollections[1].SetActive(true);
        // 챕터 1
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH1_1"))
            EndingCollections[2].SetActive(true);
        // 챕터 2
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH2_1"))
            EndingCollections[3].SetActive(true);
        // 챕터 3
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH3_1"))
            EndingCollections[4].SetActive(true);
        // 챕터 4
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH4_1"))
            EndingCollections[5].SetActive(true);
        // 챕터 4-1
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH4_1_1"))
            EndingCollections[6].SetActive(true);
        // 챕터 4-2
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH4_2_1"))
            EndingCollections[7].SetActive(true);
        // 챕터 4-3
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH4_3_1"))
            EndingCollections[8].SetActive(true);
        // 챕터 5
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH5_1"))
            EndingCollections[9].SetActive(true);
        // 챕터 6_1
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH6_1"))
            EndingCollections[10].SetActive(true);
        // 챕터 6_2
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH6_2"))
            EndingCollections[11].SetActive(true);
        // 챕터 7
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("CH7_1"))
            EndingCollections[12].SetActive(true);

        //엔딩은 임의의 엔딩 이름으로 넣어진걸 가지고 판단

        // 은지 엔딩 해금
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("Eunji_Ending"))
            EndingCollections[13].SetActive(true);
        // 준병 엔딩 해금
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("Junbyeong_Ending"))
            EndingCollections[14].SetActive(true);
        // 민석 엔딩 해금
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("Minseok_Ending"))
            EndingCollections[15].SetActive(true);
        // 아린 엔딩 해금
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("Arin_Ending"))
            EndingCollections[16].SetActive(true);
        // 솔로 엔딩 해금
        if (SaveData.P_instance._endingCollectionData._endingCollection.Contains("Solo_Ending"))
            EndingCollections[17].SetActive(true);
    }
}
