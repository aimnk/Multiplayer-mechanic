using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputService
{
    public Vector2 Axis { get; }

    bool isDashButtonUp();
}

public  class  InputService : IInputService
{
    public Vector2 Axis { get; }
     
    public bool isDashButtonUp()
    {
        throw new System.NotImplementedException();
    }
}
