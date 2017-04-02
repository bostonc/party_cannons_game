using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGlass : MonoBehaviour {

    private void OnCollisionEnter(Collision coll)
    {
        switch(coll.gameObject.tag)
        {
		case "Projectile":
				AudioDriver.S.play (SoundType.glassBreak); 
				// Note: This is to unparent the runner AND any other children (like powerups) 
				// of this destructible platform to avoid destroying the runner! (Happened during testing.)
				this.gameObject.transform.DetachChildren (); 
				Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
