using UnityEngine;

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

        BoxCollider collider = TowerPrefab.GetComponent<BoxCollider>();
        if (collider == null) {
            Debug.LogError("BoxCollider not found on Tower Prefab");
            return;
        }
        Vector3 boxCenter = collider.bounds.center;
        Vector3 boxHalfExtents = collider.bounds.extents;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GroundLayer)) {
            float towerHeight = collider.bounds.size.y/2;
            obj = Instantiate(TowerPrefab, hit.point + new Vector3(0, towerHeight, 0), Quaternion.identity);
            obj.SetActive(false);
        } else {
            return;
        }

        Collider[] overlappingColliders = Physics.OverlapBox(boxCenter, boxHalfExtents, Quaternion.identity);
        bool isOverlapping = overlappingColliders.Length > 0;

        if (isOverlapping) {
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
            if (!Physics.Raycast(point + Vector3.up * 0.1f, Vector3.down, 0.2f, GroundLayer)) {
                Debug.Log("Object not fully on ground");
                Destroy(obj);
                return;
            }
        }

        obj.SetActive(true);
    }

    public WalkNode StartNode;
    public WalkNode EndNode;

    public int MONEY = 999999;
}
