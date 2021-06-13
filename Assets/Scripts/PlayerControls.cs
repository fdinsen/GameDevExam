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
                },
                {
                    ""name"": ""TurnLeft"",
                    ""type"": ""Button"",
                    ""id"": ""ce5965b1-1d76-434d-b4a6-bc076e8e35bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TurnRight"",
                    ""type"": ""Button"",
                    ""id"": ""8b6c7c20-f159-458c-9407-5c09e3bbc0f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""e3c65e73-d853-48a0-bb97-1c4214db1ed8"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""85308527-e809-4998-9cfd-6f0ecec5d630"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff777932-84fd-45d3-8932-a4e2eea70528"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d68a96d6-83b8-4a05-8fdf-7bd6bf3d0c16"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""f50d4c72-2ca5-4b83-8104-23e2788697a7"",
            ""actions"": [
                {
                    ""name"": ""LoadInventory"",
                    ""type"": ""Button"",
                    ""id"": ""5711b3ce-6d4d-4f9c-8363-107e1c797f8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SaveInventory"",
                    ""type"": ""Button"",
                    ""id"": ""bd43be3c-8aec-4e03-b05e-aad51c7854aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""e3f533e1-5e2b-497b-9c61-a13245eb4c7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""837f2b1a-204f-402d-9714-7eff88248573"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed109738-3736-4499-9ceb-7c6436558732"",
                    ""path"": ""<Keyboard>/f5"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SaveInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7dc26232-8a92-4f0d-afba-f25013d303c2"",
                    ""path"": ""<Keyboard>/f9"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LoadInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
        m_DefaultInput_TurnLeft = m_DefaultInput.FindAction("TurnLeft", throwIfNotFound: true);
        m_DefaultInput_TurnRight = m_DefaultInput.FindAction("TurnRight", throwIfNotFound: true);
        m_DefaultInput_Interact = m_DefaultInput.FindAction("Interact", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_LoadInventory = m_Inventory.FindAction("LoadInventory", throwIfNotFound: true);
        m_Inventory_SaveInventory = m_Inventory.FindAction("SaveInventory", throwIfNotFound: true);
        m_Inventory_OpenInventory = m_Inventory.FindAction("OpenInventory", throwIfNotFound: true);
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
    private readonly InputAction m_DefaultInput_TurnLeft;
    private readonly InputAction m_DefaultInput_TurnRight;
    private readonly InputAction m_DefaultInput_Interact;
    public struct DefaultInputActions
    {
        private @PlayerControls m_Wrapper;
        public DefaultInputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @onPress => m_Wrapper.m_DefaultInput_onPress;
        public InputAction @Move => m_Wrapper.m_DefaultInput_Move;
        public InputAction @TurnLeft => m_Wrapper.m_DefaultInput_TurnLeft;
        public InputAction @TurnRight => m_Wrapper.m_DefaultInput_TurnRight;
        public InputAction @Interact => m_Wrapper.m_DefaultInput_Interact;
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
                @TurnLeft.started -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.performed -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.canceled -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnTurnLeft;
                @TurnRight.started -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnTurnRight;
                @TurnRight.performed -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnTurnRight;
                @TurnRight.canceled -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnTurnRight;
                @Interact.started -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_DefaultInputActionsCallbackInterface.OnInteract;
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
                @TurnLeft.started += instance.OnTurnLeft;
                @TurnLeft.performed += instance.OnTurnLeft;
                @TurnLeft.canceled += instance.OnTurnLeft;
                @TurnRight.started += instance.OnTurnRight;
                @TurnRight.performed += instance.OnTurnRight;
                @TurnRight.canceled += instance.OnTurnRight;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public DefaultInputActions @DefaultInput => new DefaultInputActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_LoadInventory;
    private readonly InputAction m_Inventory_SaveInventory;
    private readonly InputAction m_Inventory_OpenInventory;
    public struct InventoryActions
    {
        private @PlayerControls m_Wrapper;
        public InventoryActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LoadInventory => m_Wrapper.m_Inventory_LoadInventory;
        public InputAction @SaveInventory => m_Wrapper.m_Inventory_SaveInventory;
        public InputAction @OpenInventory => m_Wrapper.m_Inventory_OpenInventory;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @LoadInventory.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnLoadInventory;
                @LoadInventory.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnLoadInventory;
                @LoadInventory.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnLoadInventory;
                @SaveInventory.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSaveInventory;
                @SaveInventory.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSaveInventory;
                @SaveInventory.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnSaveInventory;
                @OpenInventory.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnOpenInventory;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LoadInventory.started += instance.OnLoadInventory;
                @LoadInventory.performed += instance.OnLoadInventory;
                @LoadInventory.canceled += instance.OnLoadInventory;
                @SaveInventory.started += instance.OnSaveInventory;
                @SaveInventory.performed += instance.OnSaveInventory;
                @SaveInventory.canceled += instance.OnSaveInventory;
                @OpenInventory.started += instance.OnOpenInventory;
                @OpenInventory.performed += instance.OnOpenInventory;
                @OpenInventory.canceled += instance.OnOpenInventory;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);
    public interface IDefaultInputActions
    {
        void OnOnPress(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnTurnLeft(InputAction.CallbackContext context);
        void OnTurnRight(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnLoadInventory(InputAction.CallbackContext context);
        void OnSaveInventory(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
    }
}
