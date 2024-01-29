using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[DisallowMultipleComponent]
public class InteractorEvent : MonoBehaviour
{
    public event Action<InteractorEvent, InteractorEventArgs> OnColorationFinished;
    public event Action<InteractorEvent, InteractorEventArgs> OnDecolorationFinished;

    public void CallInteractor()
    {
        OnColorationFinished?.Invoke(this, new InteractorEventArgs() { });
    }

    public void CallDecolorationInteractor()
    {
        OnDecolorationFinished?.Invoke(this, new InteractorEventArgs() { });
    }

}

public class InteractorEventArgs : EventArgs
{
    
}

