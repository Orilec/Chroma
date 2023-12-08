//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/CharacterInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @CharacterInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CharacterInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CharacterInputActions"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""074328a8-b779-4d1d-995f-8bc2823bbc24"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""a6c15a82-daf5-46a7-a7b5-b87a9d88bb1c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""c90bde59-edf8-4660-9de1-3831227ea04d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a8b49185-0f94-41a2-8fed-872526f6299b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slide"",
                    ""type"": ""Button"",
                    ""id"": ""8253607b-ae7e-431b-b737-7f293306c59a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ThrowSidekick"",
                    ""type"": ""Button"",
                    ""id"": ""5949ca71-120a-4a26-bc8d-c625701ce99e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RecenterCamera"",
                    ""type"": ""Button"",
                    ""id"": ""903919ce-911e-46cd-89a8-aecce1f5d80a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DisplayNotebook"",
                    ""type"": ""Button"",
                    ""id"": ""4acebb0a-4f7e-4c67-8147-7b367939ae43"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DebugRespawn"",
                    ""type"": ""Button"",
                    ""id"": ""96e741b0-d075-4809-8d79-737cd0a41e02"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0c4e13e8-7deb-473d-9024-26933acacaaf"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""0bc8de3f-49f4-4b35-adad-e36da6816760"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""01517c04-c32c-41b8-938f-47dc6b49bc3f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""32fd3fd6-d0fe-499f-83d2-28160320fd8e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8831ccfb-f66a-41f8-83c7-c7f94d93eb28"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1c0babfb-9772-470a-a345-7191371820bf"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1448247a-343d-428d-90d7-c99c5a91a8c7"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0cb0f21-bf53-40ce-98f1-9c6f7389ab6a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.1,y=0.1)"",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad40aa6f-2d4c-47e6-a549-2bfb0e9715aa"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3ca38a5-d269-4fca-9e1d-12f65f3f1339"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""262ec8bd-5ca8-4df0-b841-053b303799c8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fa2a0b4-9009-4c5f-963f-f45e96c971b8"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Slide"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""515b564d-4906-45f9-888c-a275e1b54bfe"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""ThrowSidekick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4e88303-8b1b-4d17-9178-04c106d09b16"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ThrowSidekick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a95dd0a-414d-4840-969b-35aad16f1e63"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""RecenterCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fcabdb86-8513-4a3d-bec7-349430bd59d3"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RecenterCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df7a28cb-f657-4582-adb6-bfb0a7a137c3"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DisplayNotebook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""572fc232-b477-4e29-8e5b-2d3bb3ffaa65"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""DisplayNotebook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4960ace4-b498-4a6f-99ed-cb064ab12d9a"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""DebugRespawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ec9f217-5ab8-4086-9004-5ea861a3d0aa"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DebugRespawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interaction"",
            ""id"": ""d1e2fe9f-eac3-49b6-a0d6-6c6c915d5228"",
            ""actions"": [
                {
                    ""name"": ""ValidateLevel"",
                    ""type"": ""Button"",
                    ""id"": ""d86471e0-0964-49b5-9183-8761f308d390"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0308cbb1-e533-48e8-9da9-7d6f4f81f43d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""ValidateLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cb311a8-131b-43f5-9f45-ad0f6efe70ba"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ValidateLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": []
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": []
        }
    ]
}");
        // CharacterControls
        m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
        m_CharacterControls_Move = m_CharacterControls.FindAction("Move", throwIfNotFound: true);
        m_CharacterControls_Look = m_CharacterControls.FindAction("Look", throwIfNotFound: true);
        m_CharacterControls_Jump = m_CharacterControls.FindAction("Jump", throwIfNotFound: true);
        m_CharacterControls_Slide = m_CharacterControls.FindAction("Slide", throwIfNotFound: true);
        m_CharacterControls_ThrowSidekick = m_CharacterControls.FindAction("ThrowSidekick", throwIfNotFound: true);
        m_CharacterControls_RecenterCamera = m_CharacterControls.FindAction("RecenterCamera", throwIfNotFound: true);
        m_CharacterControls_DisplayNotebook = m_CharacterControls.FindAction("DisplayNotebook", throwIfNotFound: true);
        m_CharacterControls_DebugRespawn = m_CharacterControls.FindAction("DebugRespawn", throwIfNotFound: true);
        // Interaction
        m_Interaction = asset.FindActionMap("Interaction", throwIfNotFound: true);
        m_Interaction_ValidateLevel = m_Interaction.FindAction("ValidateLevel", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // CharacterControls
    private readonly InputActionMap m_CharacterControls;
    private List<ICharacterControlsActions> m_CharacterControlsActionsCallbackInterfaces = new List<ICharacterControlsActions>();
    private readonly InputAction m_CharacterControls_Move;
    private readonly InputAction m_CharacterControls_Look;
    private readonly InputAction m_CharacterControls_Jump;
    private readonly InputAction m_CharacterControls_Slide;
    private readonly InputAction m_CharacterControls_ThrowSidekick;
    private readonly InputAction m_CharacterControls_RecenterCamera;
    private readonly InputAction m_CharacterControls_DisplayNotebook;
    private readonly InputAction m_CharacterControls_DebugRespawn;
    public struct CharacterControlsActions
    {
        private @CharacterInputActions m_Wrapper;
        public CharacterControlsActions(@CharacterInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterControls_Move;
        public InputAction @Look => m_Wrapper.m_CharacterControls_Look;
        public InputAction @Jump => m_Wrapper.m_CharacterControls_Jump;
        public InputAction @Slide => m_Wrapper.m_CharacterControls_Slide;
        public InputAction @ThrowSidekick => m_Wrapper.m_CharacterControls_ThrowSidekick;
        public InputAction @RecenterCamera => m_Wrapper.m_CharacterControls_RecenterCamera;
        public InputAction @DisplayNotebook => m_Wrapper.m_CharacterControls_DisplayNotebook;
        public InputAction @DebugRespawn => m_Wrapper.m_CharacterControls_DebugRespawn;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
        public void AddCallbacks(ICharacterControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Slide.started += instance.OnSlide;
            @Slide.performed += instance.OnSlide;
            @Slide.canceled += instance.OnSlide;
            @ThrowSidekick.started += instance.OnThrowSidekick;
            @ThrowSidekick.performed += instance.OnThrowSidekick;
            @ThrowSidekick.canceled += instance.OnThrowSidekick;
            @RecenterCamera.started += instance.OnRecenterCamera;
            @RecenterCamera.performed += instance.OnRecenterCamera;
            @RecenterCamera.canceled += instance.OnRecenterCamera;
            @DisplayNotebook.started += instance.OnDisplayNotebook;
            @DisplayNotebook.performed += instance.OnDisplayNotebook;
            @DisplayNotebook.canceled += instance.OnDisplayNotebook;
            @DebugRespawn.started += instance.OnDebugRespawn;
            @DebugRespawn.performed += instance.OnDebugRespawn;
            @DebugRespawn.canceled += instance.OnDebugRespawn;
        }

        private void UnregisterCallbacks(ICharacterControlsActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Slide.started -= instance.OnSlide;
            @Slide.performed -= instance.OnSlide;
            @Slide.canceled -= instance.OnSlide;
            @ThrowSidekick.started -= instance.OnThrowSidekick;
            @ThrowSidekick.performed -= instance.OnThrowSidekick;
            @ThrowSidekick.canceled -= instance.OnThrowSidekick;
            @RecenterCamera.started -= instance.OnRecenterCamera;
            @RecenterCamera.performed -= instance.OnRecenterCamera;
            @RecenterCamera.canceled -= instance.OnRecenterCamera;
            @DisplayNotebook.started -= instance.OnDisplayNotebook;
            @DisplayNotebook.performed -= instance.OnDisplayNotebook;
            @DisplayNotebook.canceled -= instance.OnDisplayNotebook;
            @DebugRespawn.started -= instance.OnDebugRespawn;
            @DebugRespawn.performed -= instance.OnDebugRespawn;
            @DebugRespawn.canceled -= instance.OnDebugRespawn;
        }

        public void RemoveCallbacks(ICharacterControlsActions instance)
        {
            if (m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICharacterControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_CharacterControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);

    // Interaction
    private readonly InputActionMap m_Interaction;
    private List<IInteractionActions> m_InteractionActionsCallbackInterfaces = new List<IInteractionActions>();
    private readonly InputAction m_Interaction_ValidateLevel;
    public struct InteractionActions
    {
        private @CharacterInputActions m_Wrapper;
        public InteractionActions(@CharacterInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @ValidateLevel => m_Wrapper.m_Interaction_ValidateLevel;
        public InputActionMap Get() { return m_Wrapper.m_Interaction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionActions set) { return set.Get(); }
        public void AddCallbacks(IInteractionActions instance)
        {
            if (instance == null || m_Wrapper.m_InteractionActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InteractionActionsCallbackInterfaces.Add(instance);
            @ValidateLevel.started += instance.OnValidateLevel;
            @ValidateLevel.performed += instance.OnValidateLevel;
            @ValidateLevel.canceled += instance.OnValidateLevel;
        }

        private void UnregisterCallbacks(IInteractionActions instance)
        {
            @ValidateLevel.started -= instance.OnValidateLevel;
            @ValidateLevel.performed -= instance.OnValidateLevel;
            @ValidateLevel.canceled -= instance.OnValidateLevel;
        }

        public void RemoveCallbacks(IInteractionActions instance)
        {
            if (m_Wrapper.m_InteractionActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInteractionActions instance)
        {
            foreach (var item in m_Wrapper.m_InteractionActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InteractionActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InteractionActions @Interaction => new InteractionActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface ICharacterControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSlide(InputAction.CallbackContext context);
        void OnThrowSidekick(InputAction.CallbackContext context);
        void OnRecenterCamera(InputAction.CallbackContext context);
        void OnDisplayNotebook(InputAction.CallbackContext context);
        void OnDebugRespawn(InputAction.CallbackContext context);
    }
    public interface IInteractionActions
    {
        void OnValidateLevel(InputAction.CallbackContext context);
    }
}
