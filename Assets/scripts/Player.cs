using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   //  [SerializeField] to make it  editable fields on unity UI
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    private bool isWalking;
    private Vector3 lastInteractDir;
    
    // Update is called once per frame
    private void Update()
    {
      HandleMovement();
      HandleInteractions();
    }

    public bool IsWalking(){
      return isWalking;
    }

    private void HandleInteractions(){
      Vector2 inputVector = gameInput.GetMovementVectorNormalized();

      Vector3 moveDir = new Vector3(inputVector.x , 0 , inputVector.y);
      if(moveDir != Vector3.zero){
        lastInteractDir = moveDir;
      }
      float interactDistance = 2f;
      if(Physics.Raycast(transform.position , lastInteractDir ,out RaycastHit raycastHit , interactDistance ,counterLayerMask)){
        if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)){
          // Has ClearClounter
          clearCounter.Interact();
        }

      }else{
        Debug.Log("-");
      }
    }
    private void HandleMovement(){

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
 
        Vector3 moveDir = new Vector3(inputVector.x , 0 , inputVector.y);

        // pysics operation to check if there is anything on the player way.
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position , transform.position + Vector3.up * playerHeight ,playerRadius , moveDir , moveDistance);
        if(!canMove){
          // Cannot move towards moveDir

          // Attempt only X movement
          Vector3 moveDirX = new Vector3(moveDir.x , 0 ,0).normalized;
          canMove = !Physics.CapsuleCast(transform.position , transform.position + Vector3.up * playerHeight ,playerRadius , moveDirX , moveDistance);
          if(canMove){
            // Can move only on X
             moveDir = moveDirX;
          }else{
            // Cannot move only on X
            // Attempt only Z movement
              Vector3 moveDirZ = new Vector3( 0 ,0 ,moveDir.z).normalized;
              canMove = !Physics.CapsuleCast(transform.position , transform.position + Vector3.up * playerHeight ,playerRadius , moveDirZ , moveDistance);
              if(canMove){
                // we can move only on Z
                moveDir = moveDirZ;
              }else{
                // That mean we cannot move on any direction
              }
          }
        }
        if(canMove){
          transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
         // player rotation , info : rotion / eulerAngles = eagles from 0 to 360
        float rotateSpeed = 10f;
         //   slerp is math funtion for smooth rotaion
        transform.forward = Vector3.Slerp(transform.forward , moveDir , Time.deltaTime * rotateSpeed);
    }
}
