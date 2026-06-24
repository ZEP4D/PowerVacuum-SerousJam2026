using UnityEngine;


namespace ArchitectureOverhaul
{
    public interface IStampInteractable
    {
        void SetHighlight(bool doHighlight, StampController withStamp) {}
        void InteractPrimary(StampController withStamp) {}
    }
}
