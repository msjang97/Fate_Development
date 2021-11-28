using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCollection : MonoBehaviour
{
    public Transform contentBox;
    public Transform illustrationBox;

    Transform[] EndingCollections; //13. 은지   14. 준병   15. 민석   16. 아린 17. 솔로

    // index 1 부터 시작 
    public enum IllustrationName
    {
        dummy,
        Title,
        엘리베이터,
        고아린_만남,
        은석놀이,
        삼자대면,
        피씨방_민석,
        준병철수,
        어린찬우아린,
        편돌이민석,
        준병_노래자랑,
        귀신의집,
        찬우은지,
        방해자집합,
        은지엔딩,
        준병엔딩,
        민석엔딩,
        아린엔딩,
        솔로엔딩,
    }

    private void Start()
    {
        EndingCollections = contentBox.GetComponentsInChildren<Transform>();

        CollectEnding();
    }

    public void CollectEnding()
    {
        if (EndingCollections == null) return;

        foreach (var item in SaveData.P_instance._endingCollectionData._endingCollection)
            EndingCollections[item].GetChild(0).gameObject.SetActive(true);
    }

    public void OnActiveIllustration(GameObject Mine)
    {
        illustrationBox.GetChild(int.Parse(Mine.name) - 1).gameObject.SetActive(true);
    }
}
