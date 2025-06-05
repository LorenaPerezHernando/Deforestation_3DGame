using System.Collections;
using System.Collections.Generic;
using Deforestation.Interaction;
using Deforestation.Machine;
using UnityEngine;

namespace Deforestation.Network
{
    public class NetworkInteractions : MachineInteraction
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Interact()
        {
            //if (_type == MachineInteractionType.Door)
            //{

            //    //Move Door
            //    transform.position = _target.position;
            //    StartCoroutine(DoorToInitialPos());
            //}
            if (_type == MachineInteractionType.Stairs)
            {
                //Teleport Player
                GameController.Instance.TeleportPlayer(_target.position);
            }
            if (_type == MachineInteractionType.Machine)
            {
                //Machine Mode
                Transform follow = _target.GetComponent<NetworkMachine>().machineFollow;
                MachineController machine = _target.GetComponent<MachineController>();
                (NetworkGameController.Instance as NetworkGameController).InitializeMachine(follow, machine);
                GameController.Instance.MachineMode(true);
            }
        }
        IEnumerator DoorToInitialPos()
        {

            _initialPosDoor.GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(5);
            transform.position = _initialPosDoor.position;
            _initialPosDoor.GetComponent<Collider>().enabled = true;

        }
    }
}
