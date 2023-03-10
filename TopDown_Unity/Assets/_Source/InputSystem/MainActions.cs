//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/_Source/InputSystem/MainActions.inputactions
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

namespace InputSystem
{
    public partial class @MainActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MainActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainActions"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""e4cb655d-2f74-4cbc-90ca-3e0e7194d781"",
            ""actions"": [
                {
                    ""name"": ""Direction"",
                    ""type"": ""Value"",
                    ""id"": ""c574a328-07e1-432a-b8bb-b1ec9e7591d6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""495afe47-7584-4592-8f83-e56f370289b1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7d1700c4-a420-4bcf-adc4-1122bdde6100"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c051084a-4c09-4de9-8250-16d0adc1c34b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d0ee6c31-d85d-4c50-8040-dfea1654f2e8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1cb1fa48-2c8f-48f2-bc26-dd5a2d03b282"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Attack"",
            ""id"": ""9c57392e-2133-43fd-a286-325d2fd834eb"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""9abb3c07-0bcb-4cfd-bb79-f887338d4a72"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hook"",
                    ""type"": ""Button"",
                    ""id"": ""b5be453c-0720-4bbe-a59a-c0344f6b1b1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""52cf077b-74c7-45ca-ad1e-22f10c416579"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5a168092-7f85-4992-a269-68225244e8b1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a6a08cd-92ac-4fb1-afbf-fbf4eaaf6163"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ed71573-7b6d-490f-aebb-e5bde9282504"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Drag"",
            ""id"": ""fcc950c0-a529-401c-9b72-665cee220d68"",
            ""actions"": [
                {
                    ""name"": ""ConnectRelease"",
                    ""type"": ""Button"",
                    ""id"": ""430b0d5b-3c02-4d8d-bd3c-d112929d1985"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1f9b900b-9b7d-4fc8-aec9-9719d432a3a2"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConnectRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Movement
            m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
            m_Movement_Direction = m_Movement.FindAction("Direction", throwIfNotFound: true);
            // Attack
            m_Attack = asset.FindActionMap("Attack", throwIfNotFound: true);
            m_Attack_Shoot = m_Attack.FindAction("Shoot", throwIfNotFound: true);
            m_Attack_Hook = m_Attack.FindAction("Hook", throwIfNotFound: true);
            m_Attack_MousePos = m_Attack.FindAction("MousePos", throwIfNotFound: true);
            // Drag
            m_Drag = asset.FindActionMap("Drag", throwIfNotFound: true);
            m_Drag_ConnectRelease = m_Drag.FindAction("ConnectRelease", throwIfNotFound: true);
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

        // Movement
        private readonly InputActionMap m_Movement;
        private IMovementActions m_MovementActionsCallbackInterface;
        private readonly InputAction m_Movement_Direction;
        public struct MovementActions
        {
            private @MainActions m_Wrapper;
            public MovementActions(@MainActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Direction => m_Wrapper.m_Movement_Direction;
            public InputActionMap Get() { return m_Wrapper.m_Movement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
            public void SetCallbacks(IMovementActions instance)
            {
                if (m_Wrapper.m_MovementActionsCallbackInterface != null)
                {
                    @Direction.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnDirection;
                    @Direction.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnDirection;
                    @Direction.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnDirection;
                }
                m_Wrapper.m_MovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Direction.started += instance.OnDirection;
                    @Direction.performed += instance.OnDirection;
                    @Direction.canceled += instance.OnDirection;
                }
            }
        }
        public MovementActions @Movement => new MovementActions(this);

        // Attack
        private readonly InputActionMap m_Attack;
        private IAttackActions m_AttackActionsCallbackInterface;
        private readonly InputAction m_Attack_Shoot;
        private readonly InputAction m_Attack_Hook;
        private readonly InputAction m_Attack_MousePos;
        public struct AttackActions
        {
            private @MainActions m_Wrapper;
            public AttackActions(@MainActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Shoot => m_Wrapper.m_Attack_Shoot;
            public InputAction @Hook => m_Wrapper.m_Attack_Hook;
            public InputAction @MousePos => m_Wrapper.m_Attack_MousePos;
            public InputActionMap Get() { return m_Wrapper.m_Attack; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(AttackActions set) { return set.Get(); }
            public void SetCallbacks(IAttackActions instance)
            {
                if (m_Wrapper.m_AttackActionsCallbackInterface != null)
                {
                    @Shoot.started -= m_Wrapper.m_AttackActionsCallbackInterface.OnShoot;
                    @Shoot.performed -= m_Wrapper.m_AttackActionsCallbackInterface.OnShoot;
                    @Shoot.canceled -= m_Wrapper.m_AttackActionsCallbackInterface.OnShoot;
                    @Hook.started -= m_Wrapper.m_AttackActionsCallbackInterface.OnHook;
                    @Hook.performed -= m_Wrapper.m_AttackActionsCallbackInterface.OnHook;
                    @Hook.canceled -= m_Wrapper.m_AttackActionsCallbackInterface.OnHook;
                    @MousePos.started -= m_Wrapper.m_AttackActionsCallbackInterface.OnMousePos;
                    @MousePos.performed -= m_Wrapper.m_AttackActionsCallbackInterface.OnMousePos;
                    @MousePos.canceled -= m_Wrapper.m_AttackActionsCallbackInterface.OnMousePos;
                }
                m_Wrapper.m_AttackActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Shoot.started += instance.OnShoot;
                    @Shoot.performed += instance.OnShoot;
                    @Shoot.canceled += instance.OnShoot;
                    @Hook.started += instance.OnHook;
                    @Hook.performed += instance.OnHook;
                    @Hook.canceled += instance.OnHook;
                    @MousePos.started += instance.OnMousePos;
                    @MousePos.performed += instance.OnMousePos;
                    @MousePos.canceled += instance.OnMousePos;
                }
            }
        }
        public AttackActions @Attack => new AttackActions(this);

        // Drag
        private readonly InputActionMap m_Drag;
        private IDragActions m_DragActionsCallbackInterface;
        private readonly InputAction m_Drag_ConnectRelease;
        public struct DragActions
        {
            private @MainActions m_Wrapper;
            public DragActions(@MainActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @ConnectRelease => m_Wrapper.m_Drag_ConnectRelease;
            public InputActionMap Get() { return m_Wrapper.m_Drag; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DragActions set) { return set.Get(); }
            public void SetCallbacks(IDragActions instance)
            {
                if (m_Wrapper.m_DragActionsCallbackInterface != null)
                {
                    @ConnectRelease.started -= m_Wrapper.m_DragActionsCallbackInterface.OnConnectRelease;
                    @ConnectRelease.performed -= m_Wrapper.m_DragActionsCallbackInterface.OnConnectRelease;
                    @ConnectRelease.canceled -= m_Wrapper.m_DragActionsCallbackInterface.OnConnectRelease;
                }
                m_Wrapper.m_DragActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ConnectRelease.started += instance.OnConnectRelease;
                    @ConnectRelease.performed += instance.OnConnectRelease;
                    @ConnectRelease.canceled += instance.OnConnectRelease;
                }
            }
        }
        public DragActions @Drag => new DragActions(this);
        public interface IMovementActions
        {
            void OnDirection(InputAction.CallbackContext context);
        }
        public interface IAttackActions
        {
            void OnShoot(InputAction.CallbackContext context);
            void OnHook(InputAction.CallbackContext context);
            void OnMousePos(InputAction.CallbackContext context);
        }
        public interface IDragActions
        {
            void OnConnectRelease(InputAction.CallbackContext context);
        }
    }
}
