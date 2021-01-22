using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEnemyMove : BehaviourTreeNode
{
   protected Vector3 NextDestination { get; set; }

   [SerializeField]
   public float speed = 5, startX, startY;


   public BTEnemyMove(BehaviourTree t, float startX, float startY): base(t)
    {
        this.startX = startX;
        this.startY = startY;
        NextDestination = Vector3.zero;
        FindNextDestination();
    }
    public bool FindNextDestination()
    {
        object o;
        bool found = false;
        found = Tree.Blackboard.TryGetValue("enemy1", out o);
        if (found)
        {
            Rect bounds = (Rect)o;
            float x = (UnityEngine.Random.value * bounds.width) + startX;
            float y = (UnityEngine.Random.value * bounds.height) + startY;
            NextDestination = new Vector3(x, y, NextDestination.z);
        }

        return found;
    }

    public override Result Execute()
    {
        //encontrar a prox destination dps de chegar a previous destination
        if(Tree.gameObject.transform.position == NextDestination)
        {
            if (!FindNextDestination())
                return Result.Failure;
            else
                return Result.Success;
        }
        else
        {
            Tree.gameObject.transform.position = Vector3.MoveTowards(Tree.gameObject.transform.position, NextDestination, Time.deltaTime * speed);

            return Result.Running;
        }
    }
}
