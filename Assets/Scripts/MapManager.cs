using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class MapManager : MonoBehaviour
{
    List<GameObject> MapTiles;
    List<(GameObject, Color)> SpawnPoints; 
    List<Color> colorArr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        colorArr = new List<Color>(){ GameManager.FireFactionColor, GameManager.WaterFactionColor, GameManager.EarthFactionColor, GameManager.PlayerFactionColor };

        MapTiles = GameObject.FindGameObjectsWithTag("MapPiece").ToList();

        SpawnPoints = (MapTiles.Where(x => x.GetComponent<MapObject>().IsSpawn)).Select(x => (x, Color.red)).OrderBy(i => Guid.NewGuid()).ToList();
        
        for(int i = 0; i < SpawnPoints.Count; i++){
            SpawnPoints[i] = (SpawnPoints[i].Item1, colorArr[i]);
            SpawnPoints[i].Item1.GetComponent<MapObject>().Claim();
        }

        bool valid = false;
        var cap = 100;
        while (!valid && cap > 0) {
            Colorize();
            if(MapTiles.Where(tile => !tile.GetComponent<MapObject>().IsClaimed).Count() == 0) {
                valid = true;
            }
            cap--;
        }

    }

    void Colorize() {
        foreach ((GameObject go, Color color) in SpawnPoints) {
            go.GetComponent<SpriteShapeRenderer>().color = color;
            MapObject mapScript = go.GetComponent<MapObject>();
            bool completedColoring = false;

            List<GameObject> neighborsCopy = new List<GameObject>(mapScript.Neighbors).OrderBy(i => Guid.NewGuid()).ToList();
            var colorCount = 0;
            var cap = 100;
            while (!completedColoring && cap > 0) {
                //Randomize Again
                neighborsCopy.OrderBy(i => Guid.NewGuid());
                //Pick a Neighbor
                GameObject curr;
                if (neighborsCopy.Count > 0) {
                    curr = neighborsCopy.First();
                } else {
                    curr = MapTiles.Where(x => x.GetComponent<MapObject>().IsClaimed == false).First();
                }
                //Color it if able
                var currentSpriteRender = curr.GetComponent<SpriteShapeRenderer>();
                var script = curr.GetComponent<MapObject>();
                if (!script.IsClaimed) {
                    currentSpriteRender.color = color;
                    script.Claim();
                    //Increment the count
                    colorCount++;

                    //Add Neighbors of the one we selected
                    //neighborsCopy.AddRange(script.Neighbors);
                }
                // remove neighbors that have already been colored
                neighborsCopy.RemoveAll(obj => obj.GetComponent<MapObject>().IsClaimed);

                //quit out once we've hit 4 colored.
                if (colorCount >= 4) {
                    completedColoring = true;
                }
                cap--;
            }
        }
    }
}
