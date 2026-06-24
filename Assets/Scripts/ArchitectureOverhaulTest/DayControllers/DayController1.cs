using UnityEngine;

using System.Collections.Generic;

namespace ArchitectureOverhaul
{
    public class DayController1 : DayController
    {
        public override List<DocumentData> GetDayDocuments(MasterControlProgram mcp)
        {
            return new();
        }
    }
}
