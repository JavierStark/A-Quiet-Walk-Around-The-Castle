using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

namespace Interactables
{
    public class Pickable : MonoBehaviour, IInteractable
    {
        void IInteractable.Interact()
        {
            Debug.Log(this.name+ " was interacted");
            Destroy(this.gameObject);
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
