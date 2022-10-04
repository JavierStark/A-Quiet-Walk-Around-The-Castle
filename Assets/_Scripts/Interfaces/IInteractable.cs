using Interactables;
using UnityEngine;

namespace _Scripts.Interfaces
{
    public interface IInteractable
    {
        void Interact(GameObject playerWhoInteract, ItemScriptable itemInHand);
    }
}
