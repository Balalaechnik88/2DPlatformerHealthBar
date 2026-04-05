using System;

public interface ICollectable
{
    event Action<ICollectable> Collected;

    void RaiseCollected();
}