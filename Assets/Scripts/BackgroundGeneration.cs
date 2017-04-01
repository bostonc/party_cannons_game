using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGeneration : MonoBehaviour
{
    public Material mat0;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public GameObject backdrop;

    Renderer r;

    // Use this for initialization
    void Start ()
    {
        r = backdrop.GetComponent<Renderer>();
        int seed = Random.Range(0, 4);
        //print("seed: " + seed);
        switch(seed)
        {
            case 0:
                r.material = mat0;
                break;
            case 1:
                r.material = mat1;
                break;
            case 2:
                r.material = mat2;
                break;
            case 3:
                r.material = mat3;
                break;
            default:
                Debug.Assert(false, "Background seed was out of bounds.");
                break;
        }
	}
}
