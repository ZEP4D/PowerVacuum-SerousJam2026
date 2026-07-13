using UnityEngine;


using ArchitectureOverhaul.Common;
namespace ArchitectureOverhaul
{
    public class StampSlotController : MonoBehaviour, IStampInteractable
    {
        public void InteractPrimary(StampController withStamp)
        {
            withStamp.SetStampState(StampState.Idle);
        }
    }
}
