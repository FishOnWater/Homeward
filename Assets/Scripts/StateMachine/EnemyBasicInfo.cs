using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicInfo : MonoBehaviour
{
    public float maxSpeed;

    abstract class EnemyBehaviour{
        //THe basic method that needs to be defined
        public abstract Steering GetSteering();
    }
        class MovementInfo{
        //Current position
        public Vector2 enPosition;
        //Current velocity and movement direction
        public Vector2 enVelocity;
        
        //Current orientation
        public float enOrientation;
        //Current torque and direction
        public float enRotation;
    }   
        class Steering{
        //incremental acceleration and direction
        public Vector2 enLinear;
        //incremental torque and direction
        public float enAngular;
    }

    class Seek : EnemyBehaviour{
        //Implement seek code
        MovementInfo targetInfo;
        float maxAcceleration;
           public override Steering GetSteering()
        {
            Steering steering = new Steering();
            steering.enLinear = targetInfo.enPosition - this.targetInfo.enPosition;
            steering.enLinear.Normalize();
            steering.enLinear *= maxAcceleration;
            steering.enAngular = 0f;
            return steering;
        }

        void Update(float time){
        targetInfo.enVelocity *= 0.95f; //linear drag
        targetInfo.enOrientation *= 0.95f; //torque drag

        Steering steering = GetSteering();

        targetInfo.enVelocity += steering.enLinear;
        targetInfo.enRotation += steering.enAngular;

        if(targetInfo.enVelocity.magnitude > 20){
            targetInfo.enVelocity.Normalize();
            targetInfo.enVelocity *= 20;
        }
    }
}
   class Chase : Seek {
       //Implement chase code
   } 
}
