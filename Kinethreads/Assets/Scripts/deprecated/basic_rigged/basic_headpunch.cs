using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class basic_headpunch : MonoBehaviour
{
    public multicomms messenger;
    public float current = 0.1f;
    public float punchtime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        messenger.sendMessage(0.0f, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("s")){
            StartCoroutine(punch());
        }
        
    }

    IEnumerator punch(){
        float elapsedTime = 0.0f;
        while (elapsedTime < punchtime)
        {
            messenger.sendMessage(current, 3);
            elapsedTime += punchtime;
            yield return new WaitForSeconds(punchtime);
        }
        messenger.sendMessage(0.0f, 3);
    }

    void reset(){
        messenger.sendMessage(0.0f, 3);
    }
}
