using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class testmotors : MonoBehaviour
{
    public comms_velo scomms;
    public TMP_Text statusText;
    public TMP_Text currText;
    int currMotor = 99;
    float currVal = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("0")){
            currMotor = 0;
            statusText.text = "Left Front";
        }
        else if (Input.GetKeyUp("1")){
            currMotor = 1;
            statusText.text = "Right Front";
        }
        else if (Input.GetKeyUp("2")){
            currMotor = 2;
            statusText.text = "Left Hip";
        }
        else if (Input.GetKeyUp("3")){
            currMotor = 3;
            statusText.text = "Right Hip";
        }
        else if (Input.GetKeyUp("4")){
            currMotor = 4;
            statusText.text = "Left Back";
        }
        else if (Input.GetKeyUp("5")){
            currMotor = 5;
            statusText.text = "Right Back";
        }
        else if (Input.GetKeyUp("6")){
            currMotor = 6;
            statusText.text = "Back";
        }
        else if (Input.GetKeyUp("7")){
            currMotor = 7;
            statusText.text = "Top Head";
        }
        else if (Input.GetKeyUp("8")){
            currMotor = 8;
            statusText.text = "Bot Head";
        }
        else if (Input.GetKeyUp("9")){
            currMotor = 99;
            statusText.text = "All Motors";
        }else if (Input.GetKeyUp("w")){
            currVal += 0.1f;
            if (currVal > 2.0f){
                currVal = 2.0f;
            }
        }else if (Input.GetKeyUp("s")){
            currVal -= 0.1f;
            if (currVal < 0.0f){
                currVal = 0.0f;
            }
        }
        else if(Input.GetKeyUp("f")){
            sendTarget(currVal);
        }
        else if(Input.GetKeyUp("g")){
            currVal = -0.2f;
            sendSlack();
        }else if(Input.GetKeyUp("o")){
            scomms.off();
            currVal = 0.0f;
        }
        currText.text = currVal.ToString();

    }

    public void sendTarget(float target){
        if (currMotor == 99){
            scomms.sendMessageAll("T " + target.ToString());
        }
        else{
            scomms.off();
            scomms.sendMessage("T " + target.ToString(), currMotor);
        }
    }

    public void sendSlack(){
        if (currMotor == 99){
            scomms.noslack();
        }
        else{
            scomms.off();
            scomms.noslack(currMotor);
        }
    }
}
