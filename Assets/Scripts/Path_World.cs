using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path_World : MonoBehaviour {
    public Transform WestFort;
    public Transform EastFort;
    public Transform PathType;
    private bool highlighted { get; set; }
    public bool Highlighted {
        get {
            return highlighted;
        }
        set {
            highlighted = value;
            foreach (var dirtBlock in dirtPath) {
                var dirt = dirtBlock.GetComponent<Dirt_World>();
                dirt.Highlighted = value;
            }
        }
    }


    List<Vector2> points;
    List<Transform> dirtPath;

	// Use this for initialization
	void Start () {
        highlighted = false;
        points = new List<Vector2>();
        dirtPath = new List<Transform>();
        var westPos = WestFort.transform.position;
        var eastPos = EastFort.transform.position;
        points.Add(new Vector2(westPos.x, westPos.y));
        points.Add(new Vector2(westPos.x + (eastPos.x - westPos.x) / 2, westPos.y));
        points.Add(new Vector2(westPos.x + (eastPos.x - westPos.x) / 2, eastPos.y));
        points.Add(new Vector2(eastPos.x, eastPos.y));

        for(int i = 1; i < points.Count; i++) {
            var lastPoint = points[i-1];
            var newPoint = points[i];

            if (lastPoint.x < newPoint.x) {
                //move right
                for (float x = lastPoint.x; x < newPoint.x; x++) {
                    var dirt = (Transform)Instantiate(PathType, new Vector2(x, lastPoint.y), Quaternion.identity);
                    dirtPath.Add(dirt);
                }
            } else {
                var lowerPoint = lastPoint.y < newPoint.y ? lastPoint : newPoint;
                var higherPoint = lastPoint.y > newPoint.y ? lastPoint : newPoint;

                for(float y = lowerPoint.y; y < higherPoint.y; y++) {
                    var dirt = (Transform)Instantiate(PathType, new Vector2(lastPoint.x, y), Quaternion.identity);
                    dirtPath.Add(dirt);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
