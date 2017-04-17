using UnityEngine;
using System.Linq;

public class CannonBallMetadata : MonoBehaviour
{
	InputManager.ControlID _fired_from_control_id = InputManager.ControlID.None;
	CannonControl _cc;

	public CannonBallMetadata () {}

	public void setMetadata(InputManager.ControlID cID, CannonControl cc) {
		_fired_from_control_id = cID;
		_cc = cc;
	}

	public InputManager.ControlID controllerThatFired() {
		return _fired_from_control_id;
	}

	public void setCannonControlMaterial(Material mat) {
		_cc.setMaterial(mat);
	}

	public Material getCannonControlMaterial() {
		return _cc.getMaterial ();
	}

	public void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "SlowDown") {
			GameObject go = Scorekeeper.S.spawnPopup ("Slow Down Powerup Destroyed!", _cc.gameObject.transform.TransformPoint(2 * Vector3.up)); 
			go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			Destroy (coll.gameObject); // No points here.		   
		} else if (coll.gameObject.tag == "SpeedUp") {
			Scorekeeper.S.Score (InputManager.S.getPlayerInfoWithControlID (_fired_from_control_id).playerID, 20);
			GameObject go = Scorekeeper.S.spawnPopup ("Speed Up Powerup Destroyed! +20", _cc.gameObject.transform.TransformPoint(2 * Vector3.up)); 
			go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			Destroy (coll.gameObject);
		} else if (coll.gameObject.tag == "JumpHigh") {
			Scorekeeper.S.Score (InputManager.S.getPlayerInfoWithControlID (_fired_from_control_id).playerID, 20);
			GameObject go = Scorekeeper.S.spawnPopup ("Jump High Powerup Destroyed! +20", _cc.gameObject.transform.TransformPoint(2 * Vector3.up)); 
			go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			Destroy (coll.gameObject);
		} else if (coll.gameObject.tag == "Points") {
			Scorekeeper.S.Score (InputManager.S.getPlayerInfoWithControlID (_fired_from_control_id).playerID, 50);
			GameObject go = Scorekeeper.S.spawnPopup ("Points! +50", _cc.gameObject.transform.TransformPoint(2 * Vector3.up)); 
			go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			Destroy (coll.gameObject);
		} else if (coll.gameObject.tag == "Shield") {
			GameObject go = Scorekeeper.S.spawnPopup ("Shield Powerup Destroyed!", _cc.gameObject.transform.TransformPoint(2 * Vector3.up)); 
			go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			Destroy (coll.gameObject); // No points here.
		} else if (coll.gameObject.tag == "FreezeCannon") {
			Scorekeeper.S.Score (InputManager.S.getPlayerInfoWithControlID (_fired_from_control_id).playerID, 50);
			GameObject go = Scorekeeper.S.spawnPopup ("Freeze Cannon Powerup Destroyed! +50", _cc.gameObject.transform.TransformPoint(2 * Vector3.up)); 
			go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
			Destroy (coll.gameObject);
		}
	}
}

