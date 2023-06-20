using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    public Define.CatingType _type;
    protected bool isInteractRightHand, isInteractleftHand;

    public virtual void Init() { }

    public virtual void Interact(Transform interactedHand) { }
    public virtual void Interact(Transform interactedHand, Transform target) { }

    public virtual void ExitInteract() { }
}
