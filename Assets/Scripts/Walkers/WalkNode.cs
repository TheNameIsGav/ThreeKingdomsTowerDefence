using UnityEngine;

public class WalkNode : MonoBehaviour
{
    public WalkNode NextNode;
    public bool IsEnd;
    public bool IsStart;

    private void Awake() {
        if (!Debug.isDebugBuild) {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
