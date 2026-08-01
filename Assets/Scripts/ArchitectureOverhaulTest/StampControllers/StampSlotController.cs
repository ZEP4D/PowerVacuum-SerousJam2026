using UnityEngine;


using ArchitectureOverhaul.Common;
namespace ArchitectureOverhaul
{
    public class StampSlotController : MonoBehaviour, IStampInteractable
    {
        public void InteractPrimary(IStampController withStamp)
        {
            withStamp.SetStampState(StampState.Idle);
        }
        public void SetHighlight(bool doHighlight, IStampController withStamp) {}
    }
}
