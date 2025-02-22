using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   //  [SerializeField] to make it  editable fields on unity UI
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    // Update is called once per frame
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
 
        Vector3 moveDir = new Vector3(inputVector.x , 0 , inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        isWalking = moveDir != Vector3.zero;
         // player rotation , info : rotion / eulerAngles = eagles from 0 to 360
        float rotateSpeed = 10f;
         //   slerp is math funtion for smooth rotaion
        transform.forward = Vector3.Slerp(transform.forward , moveDir , Time.deltaTime * rotateSpeed);

        Debug.Log(Time.deltaTime);
    }

    public bool IsWalking(){
      return isWalking;
    }
}
