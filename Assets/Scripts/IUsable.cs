using UnityEngine;

public interface IUsable
{
    void Use(Transform focusTarget);
    void Unuse(Transform focusTarget);
    bool IsBeingUsed();
}
