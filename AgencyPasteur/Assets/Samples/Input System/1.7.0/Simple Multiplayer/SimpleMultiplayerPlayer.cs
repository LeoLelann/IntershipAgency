using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMultiplayerPlayer : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 movementInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    void Update()
    {

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y) * speed * Time.deltaTime;

        transform.Translate(movement, Space.World);
    }
    public void OnTeleport(InputAction.CallbackContext context)
    {
        transform.position = new Vector3(Random.Range(-75, 75), 0.5f, Random.Range(-75, 75));
    }
}
