using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : Composite
{
    private int currentNode = 0;
    public Sequencer(BehaviourTree t, BehaviourTreeNode[] children) : base(t, children)
    {
    }


    public override Result Execute()
    {
        if (currentNode < Children.Count)
        {
            Result result = Children[currentNode].Execute();

            if (result == Result.Running)
                return Result.Running;
            else if (result == Result.Failure)
            {
                currentNode = 0;
                return Result.Failure;
            }
            else
            {
                currentNode++;
                if (currentNode < Children.Count)
                    return Result.Running;
                else
                {
                    currentNode = 0;
                    return Result.Success;
                }
            }
        }
        return Result.Success;
    }
}
