using UnityEngine;

namespace StampScripts
{
    public interface IStampInteractable
    {
        void SetHighlight(bool doHighlight) {}
        void InteractPrimary(StampController withStamp) {}
    }
}
