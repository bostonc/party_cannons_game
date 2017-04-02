using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGlass : MonoBehaviour {

    private void OnCollisionEnter(Collision coll)
    {
        switch(coll.gameObject.tag)
        {
            case "Projectile":
                AudioDriver.S.play(SoundType.glassBreak);
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
