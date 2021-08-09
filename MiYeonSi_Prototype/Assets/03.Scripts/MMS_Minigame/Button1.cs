using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Button1 : MonoBehaviour
{
    public GameObject[] Buttons = new GameObject[4];
    static public Button1 buttonInstance;
    private float time = 0;

    public bool isDone = false;
    public int ButtonX ; //-170~186
    public int Button1Y; // 
    public int Button2Y; // 
    public int Button3Y; // 
    public int Button4Y; // 


    // Start is called before the first frame update
    void Start()
    {
        buttonInstance = this;
    }

    // Update is called once per frame
    void Update()
    {   
        if (isDone == false)
        {
            getRandomInt(4, 0, 4);
            isDone = true;
            Invoke("ResetTime", 1.5f);

        }
        time += Time.deltaTime;
    }
    

    public void getRandomInt(int length, int min, int max)
    {
        int[] randArray = new int[length];
        bool isSame;

        for (int i = 0; i < length; ++i)
        {
            while (true)
            {
                randArray[i] = Random.Range(min, max); 
                isSame = false;
                for (int j = 0; j < i; ++j) 
                {                  
                    if (randArray[j] == randArray[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }
        for (int i = 0; i < length; i++)
        {          
            movePos(randArray[i], Buttons[i]); 
        }
    }

    private void ResetTime()
    {
        time = 0;
    }

    public void Click()
    {
        this.gameObject.SetActive(false);
    }


    public void movePos(int ranNum, GameObject gameObject)
    {
        if (ranNum == 0)
        {
            ButtonX = Random.Range(-115, 115);
            Button1Y = Random.Range(300, 510); // 300 ~ 510 210
            gameObject.transform.localPosition = new Vector3(ButtonX, Button1Y, 0.0f);
        }

        else if (ranNum == 1)
        {
            ButtonX = Random.Range(-115, 115);
            Button2Y = Random.Range(-120, 90);// 110 ~ 320 210
            gameObject.transform.localPosition = new Vector3(ButtonX, Button2Y, 0.0f);
        }
        else if (ranNum == 2)
        {
            ButtonX = Random.Range(-115, 115);
            Button3Y = Random.Range(-540, -330); // -280 ~ -70 210
            gameObject.transform.localPosition = new Vector3(ButtonX, Button3Y, 0.0f);
        }

        else if (ranNum == 3)
        {
            ButtonX = Random.Range(-115, 115);
            Button4Y = Random.Range(-960, -750); // -670 ~ -460 210
            gameObject.transform.localPosition = new Vector3(ButtonX, Button4Y, 0.0f);
        }
    }
}
