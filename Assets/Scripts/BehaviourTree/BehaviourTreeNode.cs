using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeNode 
{
    public enum Result {Running, Failure, Success};

    public BehaviourTree Tree { get; set; }

    public BehaviourTreeNode(BehaviourTree t)
    {
        Tree = t;
    }

    public virtual Result Execute()
    {
        return Result.Failure;
    }
}
