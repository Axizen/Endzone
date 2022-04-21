using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private Football playerInputControls;
    public Player Player;

    Vector3 inputMove;

    // x direction maps to z direction in 3d space
    public float moveForward => inputMove.x;
    public float moveRight => inputMove.y;
    public float moveMagnitude => Vector3.SqrMagnitude(inputMove);
    public Vector3 moveDirection => new Vector3(inputMove.normalized.x, 0, inputMove.normalized.y).normalized;


    private void Awake()
    {
        playerInputControls = new Football();

        playerInputControls.Player.Move.performed += context => inputMove = context.ReadValue<Vector2>();
        playerInputControls.Player.Move.canceled += context => inputMove = Vector3.zero;

        playerInputControls.Player.ResetRotation.performed += context => ResetRotation();

        playerInputControls.Player.Pause.performed += context => PauseApplication();
        playerInputControls.Player.Quit.performed += context => QuitApplication();
    }

    private void OnEnable()
    {
        playerInputControls.Player.Enable();
    }
    private void OnDisable()
    {
        playerInputControls.Player.Disable();
    }

    private void Update()
    {
        Debug.Log(moveForward);
        Debug.Log(moveRight);
    }

    //Reset player rotation if toppled
    private void ResetRotation()
    {
        Player.transform.rotation = Quaternion.Euler(0, Player.transform.rotation.y, 0);
    }

// Set game pause in the game manager
private void PauseApplication()
    {
        //gameManager.TogglePaused();
    }

    private void QuitApplication() { Application.Quit(); }
}
