// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""DefaultInput"",
            ""id"": ""5e4aef4d-65f4-4463-aa92-5a53f1df54f8"",
            ""actions"": [
                {
                    ""name"": ""onPress"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bb643e1e-3f2a-46a9-a818-8c75f6af1cbe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c9e66706-f805-4dc4-922e-ac22422ddeec"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a5005c91-1ee8-49d9-b541-de424b4090f4"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""onPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d06d571e-1834-4da8-a785-6e5fac2883e4"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c99f5382-a4b2-4163-a3ff-12f99e6f7285"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9e2876d1-cb0f-45a4-a774-ddcf236e80f7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ce8cece8-1059-41ea-8d64-decd46da887d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0242a56e-366f-43ca-b594-dbf7d2218ff9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DefaultInput
        m_DefaultInput = asset.FindActionMap("DefaultInput", throwIfNotFound: true);
        m_DefaultInput_onPress = m_DefaultInput.FindAction("onPress", throwIfNotFound: true);
        m_DefaultInput_Move = m_DefaultInput.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // DefaultInput
    private readonly InputActionMap m_DefaultInput;
    private IDefaultInputActions m_DefaultInputActionsCallbackInterface;
    private readonly InputAction m_DefaultInput_onPress;
    private readonly InputAction m_DefaultInput_Move;
    public struct DefaultInputActions
    {
        private @PlayerControls m_Wrapper;
        public DefaultInputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @onPress => m_Wrapper.m_DefaultInput_onPress;
        public InputAction @Move => m_Wrapper.m_DefaultInput_Move;
        public InputActionMap Get() { return m_Wrapper.m_DefaultInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultInputActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultInputActions instance)
        {
            if (m_Wrapper.m_DefaultInputActionsCallbackInterface != null)
            {
                @onPress.started -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnOnPress;
                @onPress.performed -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnOnPress;
                @onPress.canceled -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnOnPress;
                @Move.started -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_DefaultInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @onPress.started += instance.OnOnPress;
                @onPress.performed += instance.OnOnPress;
                @onPress.canceled += instance.OnOnPress;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public DefaultInputActions @DefaultInput => new DefaultInputActions(this);
    public interface IDefaultInputActions
    {
        void OnOnPress(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
