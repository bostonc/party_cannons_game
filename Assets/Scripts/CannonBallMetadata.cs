using UnityEngine;

public class CannonBallMetadata : MonoBehaviour
{
	int _fired_from_controller = -1;
	CannonControl _cc;

	public CannonBallMetadata () {}

	public void setMetadata(int controller, CannonControl cc) {
		_fired_from_controller = controller;
		_cc = cc;
	}

	public int controllerThatFired() {
		return _fired_from_controller;
	}

	public void setCannonControlMaterial(Material mat) {
		_cc.setMaterial(mat);
	}

	public Material getCannonControlMaterial() {
		return _cc.getMaterial ();
	}
}

