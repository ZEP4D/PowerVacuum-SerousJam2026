using UnityEngine;

public interface IStampInteractable
{
    void SetHighlight(bool doHighlight) {}
    void InteractPrimary(StampController withStamp) {}
}
