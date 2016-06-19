using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateWorld : MonoBehaviour {
    public static GenerateWorld instance;
    public Transform FortType;
    public Transform PathType;
    public List<Transform> forts;

    List<Transform> paths;
    int worldLeft = -100;
    int worldRight = 100;
    float spacing = 20;
    int heightVariance = 40;
    int clickOffset = 5;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () {
        forts = new List<Transform>();
        paths = new List<Transform>();
        int fortNum = 0;
        for (float x = worldLeft + spacing / 2; x < worldRight; x += spacing) {
            var created = (Transform)Instantiate(FortType, new Vector2(x, Random.value * heightVariance - heightVariance/2), Quaternion.identity);
            var fort = created.GetComponent<Fort_World>();
            if (fort != null) {
                fort.FortNum = fortNum++;
                fort.IsAlly = x < 0;
            }
            forts.Add(created);
        }

        Transform lastFort = null;
        foreach(var fort in forts) {
            if (lastFort == null) {
                lastFort = fort;
                continue;
            }
            var created = (Transform)Instantiate(PathType, new Vector2(
                lastFort.transform.position.x + (fort.transform.position.x - lastFort.transform.position.x)/2, 
                lastFort.transform.position.y + (fort.transform.position.y - lastFort.transform.position.y)/2), Quaternion.identity);
            var path = created.GetComponent<Path_World>();
            if (path != null) {
                path.WestFort = lastFort;
                path.EastFort = fort;
            }
            paths.Add(created);
            lastFort = fort;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            var camera = GameObject.Find("Camera").GetComponent<Camera>();
            var x = camera.ScreenToWorldPoint(Input.mousePosition).x;
            var y = camera.ScreenToWorldPoint(Input.mousePosition).y;
            foreach (var pathObj in paths) {
                var path = pathObj.GetComponent<Path_World>();
                var westPos = path.WestFort.transform.position;
                var eastPos = path.EastFort.transform.position;
                var lowerY = westPos.y < eastPos.y ? westPos.y - clickOffset : eastPos.y - clickOffset;
                var higherY = westPos.y > eastPos.y ? westPos.y + clickOffset : eastPos.y + clickOffset;
                if (path != null) {
                    if (x > westPos.x && x < eastPos.x && y > lowerY && y < higherY) {
                        path.Highlighted = !path.Highlighted;
                    } else {
                        path.Highlighted = false;
                    }
                }
            }
        }
    }
}
