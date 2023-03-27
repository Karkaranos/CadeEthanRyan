//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputActions/PlayerActions.inputactions
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

public partial class @PlayerActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Player1Actions"",
            ""id"": ""f2d86476-80b7-45da-9f6e-7c5a3ec7a2fb"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""aab1ef94-469e-4403-b290-35c81a753c52"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MoveScope"",
                    ""type"": ""Value"",
                    ""id"": ""91c32843-9dd7-4529-b4b9-4e3e2b93340d"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SwitchWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""71063ce3-672c-4559-965e-f6b7773f6c5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a56b6c53-d55b-44ed-b1d4-a119cad2beb2"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3bc5e797-affc-4a8c-bb21-d3c24d1562cc"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveScope"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82f05dcc-332a-49f3-9900-ac9e67a5a61d"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player1Actions
        m_Player1Actions = asset.FindActionMap("Player1Actions", throwIfNotFound: true);
        m_Player1Actions_Movement = m_Player1Actions.FindAction("Movement", throwIfNotFound: true);
        m_Player1Actions_MoveScope = m_Player1Actions.FindAction("MoveScope", throwIfNotFound: true);
        m_Player1Actions_SwitchWeapon = m_Player1Actions.FindAction("SwitchWeapon", throwIfNotFound: true);
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

    // Player1Actions
    private readonly InputActionMap m_Player1Actions;
    private IPlayer1ActionsActions m_Player1ActionsActionsCallbackInterface;
    private readonly InputAction m_Player1Actions_Movement;
    private readonly InputAction m_Player1Actions_MoveScope;
    private readonly InputAction m_Player1Actions_SwitchWeapon;
    public struct Player1ActionsActions
    {
        private @PlayerActions m_Wrapper;
        public Player1ActionsActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player1Actions_Movement;
        public InputAction @MoveScope => m_Wrapper.m_Player1Actions_MoveScope;
        public InputAction @SwitchWeapon => m_Wrapper.m_Player1Actions_SwitchWeapon;
        public InputActionMap Get() { return m_Wrapper.m_Player1Actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1ActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1ActionsActions instance)
        {
            if (m_Wrapper.m_Player1ActionsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_Player1ActionsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_Player1ActionsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_Player1ActionsActionsCallbackInterface.OnMovement;
                @MoveScope.started -= m_Wrapper.m_Player1ActionsActionsCallbackInterface.OnMoveScope;
                @MoveScope.performed -= m_Wrapper.m_Player1ActionsActionsCallbackInterface.OnMoveScope;
                @MoveScope.canceled -= m_Wrapper.m_Player1ActionsActionsCallbackInterface.OnMoveScope;
                @SwitchWeapon.started -= m_Wrapper.m_Player1ActionsActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.performed -= m_Wrapper.m_Player1ActionsActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.canceled -= m_Wrapper.m_Player1ActionsActionsCallbackInterface.OnSwitchWeapon;
            }
            m_Wrapper.m_Player1ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @MoveScope.started += instance.OnMoveScope;
                @MoveScope.performed += instance.OnMoveScope;
                @MoveScope.canceled += instance.OnMoveScope;
                @SwitchWeapon.started += instance.OnSwitchWeapon;
                @SwitchWeapon.performed += instance.OnSwitchWeapon;
                @SwitchWeapon.canceled += instance.OnSwitchWeapon;
            }
        }
    }
    public Player1ActionsActions @Player1Actions => new Player1ActionsActions(this);
    public interface IPlayer1ActionsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMoveScope(InputAction.CallbackContext context);
        void OnSwitchWeapon(InputAction.CallbackContext context);
    }
}
