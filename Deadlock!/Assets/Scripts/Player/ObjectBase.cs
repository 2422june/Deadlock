using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    public Define.CatingType _type;
    protected bool isInteractRightHand, isInteractLeftHand, isInteract;

    public virtual void Init() { }

    public virtual void Interact(VRController interactedHand) { }
    public virtual void Interact(Transform interactedHand, Transform target) { }

    public virtual void ExitInteract() { }
    public virtual void ExitInteract(VRController interactedHand) { }
}
