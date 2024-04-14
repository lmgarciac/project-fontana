using UnityEngine;

public enum ObjectType
{
    Notebook = 0,
    Hammer,
}

public interface IUsable
{
    void Use(Transform focusTarget);
    void Unuse(Transform focusTarget);
    bool IsBeingUsed();
    ObjectType GetObjectType();
}
