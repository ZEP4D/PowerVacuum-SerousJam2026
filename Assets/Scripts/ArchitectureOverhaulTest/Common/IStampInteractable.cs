using UnityEngine;


namespace ArchitectureOverhaul.Common
{
    public interface IStampInteractable
    {
        void SetHighlight(bool doHighlight, IStampController withStamp) {}
        void InteractPrimary(IStampController withStamp) {}
    }
}
