using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : BehaviourTreeNode
{
    public BehaviourTreeNode Child { get; set; }


    public Decorator(BehaviourTree t, BehaviourTreeNode c) : base(t)
    {
        Child = c;
    }
}
