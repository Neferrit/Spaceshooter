// GENERATED AUTOMATICALLY FROM 'Assets/GamepadInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;

public class @GamepadInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GamepadInput()
    {
        //- mělo by fungovat
        asset = InputActionAsset.FromJson(Resources.Load<TextAsset>("input.json").text);
        
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Turbo = m_Gameplay.FindAction("Turbo", throwIfNotFound: true);
        m_Gameplay_Shoot = m_Gameplay.FindAction("Shoot", throwIfNotFound: true);
        m_Gameplay_Special = m_Gameplay.FindAction("Special", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Turbo;
    private readonly InputAction m_Gameplay_Shoot;
    private readonly InputAction m_Gameplay_Special;
    public struct GameplayActions
    {
        private @GamepadInput m_Wrapper;
        public GameplayActions(@GamepadInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Turbo => m_Wrapper.m_Gameplay_Turbo;
        public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
        public InputAction @Special => m_Wrapper.m_Gameplay_Special;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Turbo.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTurbo;
                @Turbo.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTurbo;
                @Turbo.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTurbo;
                @Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Special.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecial;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Turbo.started += instance.OnTurbo;
                @Turbo.performed += instance.OnTurbo;
                @Turbo.canceled += instance.OnTurbo;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Special.started += instance.OnSpecial;
                @Special.performed += instance.OnSpecial;
                @Special.canceled += instance.OnSpecial;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnTurbo(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
    }
}
