using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Characters;

namespace Dialogue
{
    public class AIConversant : InteractableNPC
    {
        [SerializeField] Dialogue dialogue;

        public override void OnMouseDown()
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                player.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
        }
    }
}