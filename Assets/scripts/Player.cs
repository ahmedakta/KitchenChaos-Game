using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        Vector2 inputVector = new Vector2(0,0);
        if(Input.GetKey(KeyCode.W)){
           inputVector.y = +1;
        }
        if(Input.GetKey(KeyCode.S)){
           inputVector.y = -1;
        }
        if(Input.GetKey(KeyCode.A)){
           inputVector.x = -1;
        }
        if(Input.GetKey(KeyCode.D)){
           inputVector.x = +1;
        }

        // normalize or player, solved the issue when click on A and D player will go faster , so we solved this by normalizing the vecotor.
        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x , 0 , inputVector.y);
        transform.position += moveDir;

        Debug.Log(inputVector);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
