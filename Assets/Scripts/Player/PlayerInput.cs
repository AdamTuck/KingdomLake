using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }
    public bool interact { get; private set; }
    public bool interactSecondary { get; private set; }
    public bool collect { get; private set; }
    public bool space { get; private set; }
    public bool tab { get; private set; }
    public bool leftBtn { get; private set; }
    public bool rightBtn { get; private set; }
    public bool escape { get; private set; }
    public bool num1Pressed { get; private set; }
    public bool num2Pressed { get; private set; }

    private bool clear;

    private bool inputsAllowed;

    // Singleton
    private static PlayerInput instance;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    public static PlayerInput GetInstance()
    {
        return instance;
    }

    void Update()
    {
        ClearInputs();
        SetInputs();
    }

    // FixedUpdate runs independant of framerate
    private void FixedUpdate()
    {
        clear = true;
    }

    void SetInputs()
    {
        escape = escape || Input.GetKeyDown(KeyCode.Escape);

        if (inputsAllowed)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            interact = interact || Input.GetKeyDown(KeyCode.E);
            interactSecondary = interactSecondary || Input.GetKeyDown(KeyCode.F);
            collect = collect || Input.GetKeyDown(KeyCode.C);

            space = space || Input.GetKeyDown(KeyCode.Space);
            tab = tab || Input.GetKeyDown(KeyCode.Tab);

            leftBtn = leftBtn || Input.GetButtonDown("Fire1");
            rightBtn = rightBtn || Input.GetButton("Fire2");

            num1Pressed = num1Pressed || Input.GetKeyDown(KeyCode.Alpha1);
            num2Pressed = num2Pressed || Input.GetKeyDown(KeyCode.Alpha2);
        }
    }

    void ClearInputs()
    {
        if (!clear)
            return;

        horizontal = 0;
        vertical = 0;

        mouseX = 0;
        mouseY = 0;

        interact = false;
        interactSecondary = false;
        collect = false;

        space = false;
        tab = false;

        leftBtn = false;
        rightBtn = false;

        escape = false;

        num1Pressed = false;
        num2Pressed = false;
    }

    public void InputsLocked()
    {
        inputsAllowed = false;
    }

    public void InputsUnlocked()
    {
        inputsAllowed = true;
    }
}