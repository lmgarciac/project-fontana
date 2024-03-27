using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    private PlayerControls playerControls;

    public static InputManager Instance
    {
        get
        {
            // If the instance hasn't been set yet, try to find it in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<InputManager>();

                // If it's still null, create a new GameObject and attach the singleton script to it
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("InputManager");
                    _instance = managerObject.AddComponent<InputManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumping()
    {
        return playerControls.Player.Jump.triggered;
    }
}
