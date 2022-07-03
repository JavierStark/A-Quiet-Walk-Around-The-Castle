using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Player;
using UnityEngine.Serialization;

namespace Interactables
{
    public class Pickable : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemScriptable itemScriptable;
        public ItemScriptable GetItem => itemScriptable;

        void IInteractable.Interact(GameObject playerWhoInteract , ItemScriptable itemInHand)
        {
            Debug.Log(this.name+ " was interacted");
            if (playerWhoInteract.GetComponent<Inventory>().AddItem(itemScriptable)){
                Debug.Log("Added to Inventory");
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }

        void Start()
        {
            this.name = itemScriptable.name;

        }

        void Update()
        {

        }

    }
}
