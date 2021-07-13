using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotputControll : MonoBehaviour
{
    public GameObject powerBar; // 게이지바 
    public GameObject choice_lowest; // 가장 점수낮은 범위 
    public float rz; // Z축 -60~ 60 제한
    public float _rz; // 임시 rz저장
    
    float mz = 100f; //속도
    public int count = 0; //버튼 카운트 0 : 방향조절 1: 세기조절

    public float min; //Worst판 최저 y좌표
    public float max; //Woest판 최고 y좌표
    public float range;// Worst판 총 길이
    public float pos_y; //현재 하트 y좌표


    private float upPower = 0; // 게이지 
    public float powerSpeed = 0; 
    public float _powerSpeed = 0; 
    public float p_count = 0; // 게이지 속도
    public Slider powerSlider; // 총 게이지 수치
    private bool buttonDown;

    public GameObject heart; // 게이지바 

    public HeartControll heartCon;//하트 컨트롤 스크립트

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
            }
        }

        if (heartCon.stopHeart)
        {
            powerSpeed = 0;
            heartCon.stopHeart = false;
        }
        HeartStop();
    }

    void Init()
    {
        powerSlider.maxValue = 100; // 최대 60 슬라이더
        rz = 60;
        _rz = 0;
        mz = 100;
        count = 0;

        upPower = 0;
        //powerSpeed = 0;
        p_count = 70;
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
        //if (count == 1) powerSpeed = upPower;
    }

    void PowerCharging() // 게이지채우기
    {
        powerBar.SetActive(true);

        upPower += p_count * Time.deltaTime;
        powerSlider.value = upPower;

        if (powerSlider.value == powerSlider.maxValue || powerSlider.value == powerSlider.minValue)
            p_count = p_count * -1;
        powerSpeed = upPower;
        _powerSpeed = upPower;
    }

    void PowerStop() // 게이지 정지
    {
        powerSlider.value = _powerSpeed;
        HeartMovement();
        //HeartStop();
    }


    void HeartMovement() //하트이동 함수
    {
        //_powerSpeed = powerSpeed;
        if (count == 2)
        heart.SetActive(true);

        heart.transform.rotation = Quaternion.Euler(0f, 0f, _rz);

        //float mx = powerSpeed * Time.deltaTime;

        if (_rz > 0 && _rz <= 60)
        {
            heart.transform.Translate(Vector3.left * powerSpeed*10 * Time.deltaTime);
            heart.transform.Translate(Vector3.up * powerSpeed * 30 * Time.deltaTime);

        }

        else if (_rz < 0 && _rz >= -60)
        {
            heart.transform.Translate(Vector3.right * powerSpeed * 10 * Time.deltaTime);
            heart.transform.Translate(Vector3.up * powerSpeed * 30 * Time.deltaTime);
        }

    }
   
   void HeartStop()
    {
        min = choice_lowest.transform.position.y- 525;
        max = choice_lowest.transform.position.y + 525;
        range = max - min;

        pos_y = heart.transform.position.y;
        
       
        if (min < heart.transform.position.y && max > heart.transform.position.y)
        {
            switch ((int)_powerSpeed % 100 / 10)
            {
                case 0:
                    if (range / 10 * 0 + min == (int)heart.transform.position.y)
                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                    break;
                case 1:
                    if (range / 10 * 1 + min == (int)heart.transform.position.y)
                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                        break;
                case 2:
                    if (range / 10 * 2 + min == (int)heart.transform.position.y)
                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                    break;
                case 3:
                    if (range / 10 * 3 + min == (int)heart.transform.position.y)
                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                    break;
                case 4:
                    if (range / 10 * 4 + min == (int)heart.transform.position.y)
                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                    break;
                case 5:
                    if (range / 10 * 5 + min == (int)heart.transform.position.y)
                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                    break;
                case 6:
                    if (range / 10 * 6 + min == (int)heart.transform.position.y)
                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                    break;
                case 7:
                    if (range / 10 * 7 + min == (int)heart.transform.position.y)
                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                    break;
                case 8:
                    if (range / 10 * 8 + min == (int)heart.transform.position.y)
                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                    break;
                case 9:
                    if (range / 10 * 9 + min == (int)heart.transform.position.y)
                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                    break;
                case 10:
                    if (range / 10 * 10 + min == (int)heart.transform.position.y)

                    {
                        powerSpeed = 0;
                        heartCon.movestopHeart = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }

}
