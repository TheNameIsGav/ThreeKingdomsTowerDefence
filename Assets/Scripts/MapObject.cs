using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Claim() {
        IsClaimed = true;
    }

    public List<GameObject> Neighbors;
    public bool IsSpawn;
    public bool IsClaimed;
    public Faction Faction;
    public GameObject MapPrefab;
    public GameObject TowerPrefab;
}
