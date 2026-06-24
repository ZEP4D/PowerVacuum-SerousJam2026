using UnityEngine;
using UnityEngine.Scripting;

using System;
using System.Collections.Generic;


namespace ArchitectureOverhaul
{
    public class MasterControlProgram : MonoBehaviour
    {
        // --== SERIALIZED FIELDS ==-- //
            [SerializeReference] List<DayController> m_dayControllers;
            [SerializeField] IStampInteractable m_stampArea;
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
