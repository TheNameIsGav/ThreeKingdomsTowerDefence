using System.Linq;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents the class that handles the processing and logic of a Node, including the battlemap and difficulty scaling. 
/// </summary>
public class NodeManager : MonoBehaviour
{
    public GameObject TowerPrefab;
    public LayerMask GroundLayer;


    private void Awake() {
        GameManager.CurrentNodeManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            PlaceTower();
        }
    }

    void PlaceTower() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;
        RaycastHit hit;
        GameObject obj;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GroundLayer)) {
            obj = Instantiate(TowerPrefab, hit.point, Quaternion.identity);
        } else {
            return;
        }

        BoxCollider collider = obj.GetComponent<BoxCollider>();
        if (collider == null) {
            Debug.LogError("BoxCollider not found on Tower Prefab");
            return;
        }

        obj.transform.position = obj.transform.position + new Vector3(0, collider.size.y / 2 + .5f, 0);
        Vector3 boxCenter = obj.transform.position;
        Vector3 boxHalfExtents = collider.bounds.extents;

        List<Collider> collision = Physics.OverlapBox(obj.transform.position, obj.transform.localScale / 2).ToList();
        collision.Remove(obj.gameObject.GetComponent<BoxCollider>());
        if (collision.Count > 0) {
            Debug.Log("Object overlaps with another collider");
            Destroy(obj);
            return;
        }

        Vector3[] checkPoints = {
            boxCenter + new Vector3(boxHalfExtents.x, 0, boxHalfExtents.z),
            boxCenter + new Vector3(-boxHalfExtents.x, 0, boxHalfExtents.z),
            boxCenter + new Vector3(boxHalfExtents.x, 0, -boxHalfExtents.z),
            boxCenter + new Vector3(-boxHalfExtents.x, 0, -boxHalfExtents.z)
        };
        foreach (var point in checkPoints) {
            if (!Physics.Raycast(point + Vector3.up * 0.1f, Vector3.down, Mathf.Infinity, GroundLayer)) {
                Debug.Log("Object not fully over ground");
                Destroy(obj);
                return;
            }
        }
    }


    public WalkNode StartNode;
    public WalkNode EndNode;

    public int MONEY = 999999;
}
