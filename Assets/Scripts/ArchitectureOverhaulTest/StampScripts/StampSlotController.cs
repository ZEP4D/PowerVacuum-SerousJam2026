using UnityEngine;

namespace StampScripts
{
    public class StampSlotController : MonoBehaviour, IStampInteractable
    {
        public void InteractPrimary(StampController withStamp)
        {
            withStamp.SetStampState(StampState.Idle);
        }
    }
}
