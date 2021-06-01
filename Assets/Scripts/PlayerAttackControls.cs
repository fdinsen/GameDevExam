// GENERATED AUTOMATICALLY FROM 'Assets/PlayerAttackControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerAttackControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerAttackControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerAttackControls"",
    ""maps"": [
        {
            ""name"": ""DefaultInput"",
            ""id"": ""bf618987-b184-48c8-98c3-d82eb9bb75e4"",
            ""actions"": [
                {
                    ""name"": ""BaseAttack"",
                    ""type"": ""Button"",
                    ""id"": ""f5cbdbe3-081e-434b-89c1-e3633946ab24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AlternateAttack"",
                    ""type"": ""Button"",
                    ""id"": ""d1c8e8fd-659f-418b-9faf-672e9eef5154"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""90f9010a-6afe-4e91-864c-298a92340318"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BaseAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c320ece7-27ef-42ba-a9e4-7521c7b0b2f1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard/Mouse"",
                    ""action"": ""AlternateAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard/Mouse"",
            ""bindingGroup"": ""Keyboard/Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // DefaultInput
        m_DefaultInput = asset.FindActionMap("DefaultInput", throwIfNotFound: true);
        m_DefaultInput_BaseAttack = m_DefaultInput.FindAction("BaseAttack", throwIfNotFound: true);
        m_DefaultInput_AlternateAttack = m_DefaultInput.FindAction("AlternateAttack", throwIfNotFound: true);
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
    private readonly InputAction m_DefaultInput_BaseAttack;
    private readonly InputAction m_DefaultInput_AlternateAttack;
    public struct DefaultInputActions
    {
        private @PlayerAttackControls m_Wrapper;
        public DefaultInputActions(@PlayerAttackControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @BaseAttack => m_Wrapper.m_DefaultInput_BaseAttack;
        public InputAction @AlternateAttack => m_Wrapper.m_DefaultInput_AlternateAttack;
        public InputActionMap Get() { return m_Wrapper.m_DefaultInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultInputActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultInputActions instance)
        {
            if (m_Wrapper.m_DefaultInputActionsCallbackInterface != null)
            {
                @BaseAttack.started -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnBaseAttack;
                @BaseAttack.performed -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnBaseAttack;
                @BaseAttack.canceled -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnBaseAttack;
                @AlternateAttack.started -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnAlternateAttack;
                @AlternateAttack.performed -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnAlternateAttack;
                @AlternateAttack.canceled -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnAlternateAttack;
            }
            m_Wrapper.m_DefaultInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @BaseAttack.started += instance.OnBaseAttack;
                @BaseAttack.performed += instance.OnBaseAttack;
                @BaseAttack.canceled += instance.OnBaseAttack;
                @AlternateAttack.started += instance.OnAlternateAttack;
                @AlternateAttack.performed += instance.OnAlternateAttack;
                @AlternateAttack.canceled += instance.OnAlternateAttack;
            }
        }
    }
    public DefaultInputActions @DefaultInput => new DefaultInputActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard/Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IDefaultInputActions
    {
        void OnBaseAttack(InputAction.CallbackContext context);
        void OnAlternateAttack(InputAction.CallbackContext context);
    }
}
