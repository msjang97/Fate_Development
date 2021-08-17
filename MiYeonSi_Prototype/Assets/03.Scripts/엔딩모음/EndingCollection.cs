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
