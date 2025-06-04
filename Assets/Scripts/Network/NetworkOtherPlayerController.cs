using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Network
{
    public class NetworkOtherPlayerController : MonoBehaviour
    {
        private Animator _anim;
        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        void Update()
        {
           
        }
    }
}
