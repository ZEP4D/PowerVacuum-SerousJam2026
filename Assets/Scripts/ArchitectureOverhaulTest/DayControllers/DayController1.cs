using UnityEngine;

using System.Collections.Generic;


using ArchitectureOverhaul.Common;
namespace ArchitectureOverhaul
{
    public class DayController1 : IDayController
    {
        public List<DocumentData> GetDayDocuments(IMCP mcp)
        {
            return new();
        }
    }
}
