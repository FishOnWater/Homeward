using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{

    private BehaviourTreeNode mRoot;
    private BehaviourTreeNode m2Root;
    private bool startedBehaviour;
    private Coroutine behaviour;
    [SerializeField]
    string enemy_name;

    public Dictionary<string, object> Blackboard { get; set; }

    public BehaviourTreeNode Root { get { return mRoot; } }
    // Start is called before the first frame update
    void Start()
    {

        Blackboard = new Dictionary<string, object>();
        Blackboard.Add("enemy1", new Rect(25, 85, 10, 10));
        
        //initial behaviour is stopped
        startedBehaviour = false;
        mRoot = new Repeater(this, new Sequencer(this, new BehaviourTreeNode[] { new BTEnemyMove(this, 25, 85) }));
    }

    // Update is called once per frame
    void Update()
    {
        if (!startedBehaviour)
        {
            behaviour = StartCoroutine(RunBehaviour());
            startedBehaviour = true;
        }
    }
    private IEnumerator RunBehaviour()
    {
        BehaviourTreeNode.Result result = Root.Execute();
        while (result == BehaviourTreeNode.Result.Running)
        {
            //Debug.Log("Root result: " + result);
            yield return null;
            result = Root.Execute();
        }

        //Debug.Log("Behaviour has finished with: " + result);
    }
}
