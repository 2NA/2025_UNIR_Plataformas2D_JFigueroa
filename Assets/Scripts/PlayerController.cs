using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovementController
{
    protected override void Update()
    {
        UpdateRawMove();

        base.Update();
    }

    private void UpdateRawMove()
    {
        Vector2 rawMove = Vector2.zero;

        if (Keyboard.current.aKey.isPressed)
        {
            rawMove += Vector2.left;
        } else if (Keyboard.current.dKey.isPressed)
        {
            rawMove += Vector2.right;
        }

        desiredMove = rawMove;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            mustJump = true;
        }

        if (Keyboard.current.rightAltKey.wasPressedThisFrame)
        {
            mustPunch = true;
        }
    }
}
