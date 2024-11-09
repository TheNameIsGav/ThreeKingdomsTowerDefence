using System;
using UnityEngine;

public class WalkerBase : MonoBehaviour
{

    protected float WalkSpeed = .1f;
    protected int Reward = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentNode = GameManager.CurrentNodeManager.StartNode;
        transform.position = currentNode.transform.position;
    }

    private WalkNode currentNode;

    // Update is called once per frame
    void Update()
    {
        //If we're at the next node, then setup the next one if able
        if (transform.position == currentNode.transform.position) {
            if (currentNode.IsEnd) {
                Destroy(gameObject);
                Debug.Log("Enemy made it to the end");
            } else {
                currentNode = currentNode.NextNode;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, currentNode.transform.position, WalkSpeed);


    }
}
