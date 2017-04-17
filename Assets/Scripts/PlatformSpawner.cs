using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

	public GameObject platform;
	public GameObject platform2;
    public GameObject platform_glass;
    public int spawnRate = 250;


	private int frameCounter = 1;
	private List<GameObject> platforms = new List<GameObject>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


	}


	void FixedUpdate() {

		//Procedural platform generation
		frameCounter--;
		if (frameCounter == 0)
        {
            int platSelect = Random.Range(0, 100);            
            int yPos = Random.Range (0, 4);
			Vector3 startPos = Vector3.zero;
			if (yPos == 0 || yPos == 3) {
				startPos = new Vector3 (40, 1, 20);
			} else if (yPos == 1) {
				startPos = new Vector3 (-40, 5, 20);
			} else if (yPos == 2) {
				startPos = new Vector3 (40, 9, 20);
			}
            //gen normal
            GameObject go;
			if (yPos == 1) {

				go = Instantiate(platform2, startPos, Quaternion.identity);
				platforms.Add (go);
				frameCounter = spawnRate;
			} else {
		        if (platSelect < 80)
		        {
		            go = Instantiate(platform, startPos, Quaternion.identity);
		        }
		        else //gen glass
		        {
		            go = Instantiate(platform_glass, startPos, Quaternion.identity);
		        }			

				GameObject powerupGOref = PowerUp.S.getPowerUpInstanceForSpawning ();
				if (powerupGOref != null) {
					GameObject powerupGO = MonoBehaviour.Instantiate (powerupGOref, startPos + 2*Vector3.up, Quaternion.identity, go.transform);
					powerupGO.transform.localScale = new Vector2 (0.3f, 0.75f);
					// Note: The powerups are a child of the platforms! 
				}
				platforms.Add (go);
				frameCounter = spawnRate;
			}
		}
	}
}
