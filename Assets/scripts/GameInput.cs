using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
   private PlayerInputActions playerInputActions;
   private void Awake(){
      playerInputActions = new PlayerInputActions();
      playerInputActions.Player.Enable();
   }

    // Start is called before the first frame update
    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); 

        // normalize or player, solved the issue when click on A and D player will go faster , so we solved this by normalizing the vecotor.
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
