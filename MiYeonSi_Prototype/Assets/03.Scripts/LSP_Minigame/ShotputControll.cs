using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotputControll : MonoBehaviour
{
    public GameObject powerBar; // 게이지바 
    public float rz; // Z축 -60~ 60 제한
    public float _rz; // 임시 rz저장
    
    float mz = 100f; //속도
    public int count = 0; //버튼 카운트 0 : 방향조절 1: 세기조절

    private float upPower = 0; // 게이지 
    private float _upPower = 0; // 게이지 
    private float p_count = 10; // 게이지 속도
    public Slider TimeSlider;
    private bool buttonDown;

    public GameObject heart; // 게이지바 


    private void Start()
    {
        Init();
    }

    void Update()
    {
        if (count == 0)
            ArrowRotatation();
        else if (count >= 1)
        {
            ArrowStop();

            if (buttonDown)
                PowerCharging();
            else {
                PowerStop();
               // HeartMovement();
            }
        }
        
    }

    void Init()
    {
        TimeSlider.maxValue = 60; // 최대 60 슬라이더
        rz = 60;
        _rz = 0;
        mz = 100;
        count = 0;

        upPower = 0;
        _upPower = 0;
        p_count = 30;
    }

    public void PointerDown()
    {
        if (count == 1) 
            buttonDown = true;
    }

    public void PointerUp()
    {
        if (count == 1)
            buttonDown = false;
    }

    void ArrowRotatation() //화살표 회전
    {
        rz = Mathf.Clamp(rz, -60, 60);

        if (rz == 60 || rz ==-60) mz = mz * -1;

        rz += mz * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, rz);

    }

    void ArrowStop() // 화살표 위치 정지
    {
        transform.rotation = Quaternion.Euler(0f, 0f, _rz);
    }

    public void Stop() // 정지버튼 
    {
        count++;
        _rz = rz;
        //if (count == 1) _upPower = upPower;
    }

    void PowerCharging() // 게이지채우기
    {
        powerBar.SetActive(true);

        upPower += p_count * Time.deltaTime;
        TimeSlider.value = upPower;

        if (TimeSlider.value == TimeSlider.maxValue || TimeSlider.value == TimeSlider.minValue)
            p_count = p_count * -1;
        _upPower = upPower;
    }

    void PowerStop() // 게이지 정지
    {
        TimeSlider.value = _upPower;
        HeartMovement();

    }


    void HeartMovement() //하트이동 함수
    {
        if(count == 2)
        heart.SetActive(true);

        heart.transform.rotation = Quaternion.Euler(0f, 0f, _rz);

        //float mx = _upPower * Time.deltaTime;

        if (heart.transform.rotation.z >= 0 && heart.transform.rotation.z <= 60)
        {
            heart.transform.Translate(Vector3.left * _upPower*10 * Time.deltaTime);
            heart.transform.Translate(Vector3.up * _upPower * 30 * Time.deltaTime);

        }

        else if (heart.transform.rotation.z <= 0 && heart.transform.rotation.z >= -60)
        {
            heart.transform.Translate(Vector3.right * _upPower * 10 * Time.deltaTime);
            heart.transform.Translate(Vector3.up * _upPower * 30 * Time.deltaTime);
        }

    }

}
