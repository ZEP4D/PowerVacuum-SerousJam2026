using UnityEngine;

using System.Collections.Generic;

using ControlScripts;

namespace DayControllers
{
    public class DayController1 : DayController
    {
        public override List<DocumentData> GetDayDocuments(MasterControlProgram mcp)
        {
            return new();
        }
    }
}
