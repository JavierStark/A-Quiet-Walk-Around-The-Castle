using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Player;

namespace Interactables
{
    public class Pickable : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemScriptable _itemScriptable;
        public ItemScriptable GetItem => _itemScriptable;

        void IInteractable.Interact(GameObject playerWhoInteract)
        {
            Debug.Log(this.name+ " was interacted");
            if (playerWhoInteract.GetComponent<Inventory>().AddItem(_itemScriptable)){
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
            this.name = _itemScriptable.name;

        }

        void Update()
        {

        }

    }
}
