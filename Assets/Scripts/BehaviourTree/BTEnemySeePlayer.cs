using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEnemySeePlayer : BehaviourTreeNode
{
    GameObject player;

    public BTEnemySeePlayer(BehaviourTree t) : base(t)
    {
    }


    private bool FindTarget()
    {
        bool found = false;
        float targetRange = 10f;
        if (Vector3.Distance(Tree.gameObject.transform.position, player.transform.position) < targetRange)
        {
            found = true;
        }
        else
            found = false;
        return found;
    }

    public override Result Execute()
    {
        if (!FindTarget())
        {
            return Result.Running;
        }
        else
        {
            return Result.Success;
            Debug.Log("player found");
        }

    }
}
