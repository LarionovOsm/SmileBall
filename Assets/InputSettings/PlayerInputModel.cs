//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputSettings/PlayerInputModel.inputactions
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

public partial class @PlayerInputModel : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputModel()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputModel"",
    ""maps"": [
        {
            ""name"": ""BallControl"",
            ""id"": ""51da55e9-4e00-4a21-b728-c4ffd2d21718"",
            ""actions"": [
                {
                    ""name"": ""TouchScreen"",
                    ""type"": ""Button"",
                    ""id"": ""1d6d774b-9190-4c20-bb54-191437e937bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""82f56509-df98-4212-9efb-e68f019d6759"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59dd8d68-af38-4177-9753-de87166fd897"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchScreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BallControl
        m_BallControl = asset.FindActionMap("BallControl", throwIfNotFound: true);
        m_BallControl_TouchScreen = m_BallControl.FindAction("TouchScreen", throwIfNotFound: true);
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

    // BallControl
    private readonly InputActionMap m_BallControl;
    private IBallControlActions m_BallControlActionsCallbackInterface;
    private readonly InputAction m_BallControl_TouchScreen;
    public struct BallControlActions
    {
        private @PlayerInputModel m_Wrapper;
        public BallControlActions(@PlayerInputModel wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchScreen => m_Wrapper.m_BallControl_TouchScreen;
        public InputActionMap Get() { return m_Wrapper.m_BallControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BallControlActions set) { return set.Get(); }
        public void SetCallbacks(IBallControlActions instance)
        {
            if (m_Wrapper.m_BallControlActionsCallbackInterface != null)
            {
                @TouchScreen.started -= m_Wrapper.m_BallControlActionsCallbackInterface.OnTouchScreen;
                @TouchScreen.performed -= m_Wrapper.m_BallControlActionsCallbackInterface.OnTouchScreen;
                @TouchScreen.canceled -= m_Wrapper.m_BallControlActionsCallbackInterface.OnTouchScreen;
            }
            m_Wrapper.m_BallControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchScreen.started += instance.OnTouchScreen;
                @TouchScreen.performed += instance.OnTouchScreen;
                @TouchScreen.canceled += instance.OnTouchScreen;
            }
        }
    }
    public BallControlActions @BallControl => new BallControlActions(this);
    public interface IBallControlActions
    {
        void OnTouchScreen(InputAction.CallbackContext context);
    }
}