using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{

    private PlayerControls m_playerControls;
    // Start is called before the first frame update
    void OnEnable()
    {
        m_playerControls.DefaultInput.Enable();
    }
    
    void OnDisable()
    {
        m_playerControls.DefaultInput.Disable();
    }

    void Awake()
    {
        m_playerControls = new PlayerControls();
        m_playerControls.DefaultInput.onPress.performed += DoStuff;
    }

    // Update is called once per frame
    void Update()
    {
        if( Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Mouse Pressed!");
        }
        if(Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame)
        {
            Debug.Log("K was pressed!");
        }

        if (Pointer.current != null)
        {
            if(Pointer.current.press.wasPressedThisFrame)
            {
                var cam = Camera.main;

                Debug.Log("Pointer pressed at: " + Pointer.current.position.ReadValue());
                var ray = cam.ScreenPointToRay(Pointer.current.position.ReadValue());
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }

    void DoStuff(InputAction.CallbackContext ctx)
    {
        Debug.Log("Do stuff");
    }
}
