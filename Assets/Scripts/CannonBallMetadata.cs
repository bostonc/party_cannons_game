using UnityEngine;

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
}

