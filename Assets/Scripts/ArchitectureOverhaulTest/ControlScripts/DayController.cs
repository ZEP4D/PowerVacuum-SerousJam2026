using UnityEngine;
using System.Collections.Generic;


namespace ControlScripts
{
    public abstract class DayController : MonoBehaviour
    {
        public abstract List<DocumentData> GetDayDocuments(MasterControlProgram mcp);
    }
}
