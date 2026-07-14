using UnityEngine;

using System.Collections.Generic;


namespace ArchitectureOverhaul.Common
{
    public interface IDayController
    {
        public List<IDocumentProposition> GetDayPropositions(IMCP mcp);
    }
}
