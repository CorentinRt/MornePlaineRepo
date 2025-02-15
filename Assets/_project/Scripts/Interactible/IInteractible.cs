using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    public abstract bool IsInteractible();
    public abstract void Interact();

    public abstract bool IsGettable();
    public abstract IInteractible InteractGet();
}
