using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.VisualScripting;
using UnityEngine;

 
    public  class InputController:MonoBehaviour
    {
    public static InputController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
        private void Update()
        {
            
        }
        public Vector3 getDirection()
        {
            var horizontal = UnityEngine.Input.GetAxis("Horizontal");
            var vertical = UnityEngine.Input.GetAxis("Vertical");
            return new Vector3(horizontal, 0, vertical).normalized;
        }

        public bool IsForwardPress()
        {
            return UnityEngine.Input.GetKey(KeyCode.W);
        }
        public bool IsLeftPress()
        {
            return UnityEngine.Input.GetKey(KeyCode.A);
        }
        public bool IsBacktPress()
        {
            return UnityEngine.Input.GetKey(KeyCode.S);
        }
        public bool IsRighttPress()
        {
            return UnityEngine.Input.GetKey(KeyCode.D);
        }
        public bool IsRunPress()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }
}
 
