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
				Transform t = this.gameObject.transform.FindChild ("Runner");
				if (t != null) { 
					t.parent = null;
				}
				Destroy(this.gameObject); // Will destroy powerups on this platform as well. (See note above.)
                break;
            default:
                break;
        }
    }
}
