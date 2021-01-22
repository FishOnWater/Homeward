using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite : BehaviourTreeNode
{
    public List<BehaviourTreeNode> Children { get; set; }

    public Composite(BehaviourTree t, BehaviourTreeNode [] nodes) : base(t)
    {
        Children = new List<BehaviourTreeNode>(nodes);
    }
}
