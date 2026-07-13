using UnityEngine;

using System.Collections.Generic;


namespace ArchitectureOverhaul.Common
{
    public interface IDayController
    {
        public List<DocumentData> GetDayDocuments(IMCP mcp);
    }
}
