using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nephenthesys
{
    public class ControllerInputs : MonoBehaviour
    {
        public static ControllerInputs instance;
        public PlayerInput playerInput;
        public Vector2 movimento;
        public bool configureInputsOnStart = true;
        public bool atirar;
        public bool especial;
        public bool start;
        public bool cancel;
        public List<string> inputsBasicos;
        private List<string> inputsAtivos;

        public InputAction.CallbackContext Start { set => start = value.ReadValue<float>() != 0; }
        public InputAction.CallbackContext Cancel { set => cancel = value.ReadValue<float>() != 0; }

        public InputAction.CallbackContext Movimento
        {
            set
            {
                if (inputsAtivos.Contains("movimento"))
                    movimento = value.ReadValue<Vector2>();
            }
        }

        public InputAction.CallbackContext Atirar
        {
            set
            {
                if (inputsAtivos.Contains("atirar"))
                    atirar = value.ReadValue<float>() != 0;
            }
        }

        public InputAction.CallbackContext Especial
        {
            set
            {
                if (inputsAtivos.Contains("especial"))
                    especial = value.ReadValue<float>() != 0;
            }
        }

        private void Awake()
        {
            instance = this;
            inputsAtivos = new List<string>();
            if (configureInputsOnStart)
                ResetImputsBasicos();
        }

        public void ResetImputsBasicos()
        {
            ConfigInputsBasicos(inputsBasicos.ToArray());
        }

        public void ConfigInputsBasicos(string[] listaInputs)
        {
            inputsAtivos.Clear();
            inputsAtivos.AddRange(listaInputs);
        }

        public void Lost()
        {
            print(" Lost > ");
        }
    }
}
