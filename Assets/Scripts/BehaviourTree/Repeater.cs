using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : Decorator
{
    public Repeater(BehaviourTree t, BehaviourTreeNode c) : base(t, c)
    {
    }
    public override Result Execute()
    {
        Debug.Log("Child Returned: " + Child.Execute());
        return Result.Running;
    }
}
