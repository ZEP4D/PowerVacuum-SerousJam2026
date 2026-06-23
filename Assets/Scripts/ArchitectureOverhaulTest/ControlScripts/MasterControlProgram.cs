using UnityEngine;
using UnityEngine.Scripting;

using System;
using System.Collections.Generic;

using StampScripts;
using System.Runtime.Serialization;

namespace ControlScripts
{
    public class MasterControlProgram : MonoBehaviour
    {
        // --== SERIALIZED FIELDS ==-- //
            [SerializeReference] List<DayController> m_dayControllers;
            [SerializeField] StampAreaController m_stampArea;
        // ==--


        // --== CLASS FIELDS ==-- //
        // ==--


        // --== ATTACHED COMPONENTS ==-- //
        // ==--


        // --== CLASS METHODS ==-- //
        // ==--


        // --== UNITY METHODS ==-- //
        // ==--
    }
}
