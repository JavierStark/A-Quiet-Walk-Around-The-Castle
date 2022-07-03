using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        void Interact(GameObject playerWhoInteract, ItemScriptable itemInHand);
    }
}
