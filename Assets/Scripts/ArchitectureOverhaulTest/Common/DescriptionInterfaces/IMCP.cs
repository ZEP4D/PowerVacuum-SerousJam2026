using UnityEngine;

namespace ArchitectureOverhaul.Common
{
    public interface IMCP
    {
        void DocumentStampAreaInteracted(IStampController withStamp)
        {
            Debug.Log(
                "MCP recieved event: Document Stamp Area Stamped/nUsing stamp: '"
                + (withStamp as MonoBehaviour).gameObject.name
                + "', with type: '"
                + withStamp.GetStampType()
                + "'"
            );
        }
    }
}
