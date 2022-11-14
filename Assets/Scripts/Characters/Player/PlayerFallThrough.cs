using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFallThrough : MonoBehaviour
{
	public bool fallThrough;

    private void Update() {
        if (Keyboard.current.sKey.isPressed) 
            fallThrough = true;
        else 
            fallThrough = false;
    }
}
