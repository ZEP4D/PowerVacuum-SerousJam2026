using UnityEngine;

using System.Collections.Generic;


using ArchitectureOverhaul.Common;
namespace ArchitectureOverhaul
{
    public class DayController1 : MonoBehaviour, IDayController
    {
        public List<IDocumentProposition> GetDayPropositions(IMCP mcp)
        {
            return new();
        }
    }
}
