using UnityEngine;
using UnityEngine.Scripting;

using System;
using System.Collections.Generic;


namespace ArchitectureOverhaul
{
    public class MasterControlProgram : MonoBehaviour
    {
        // --== SERIALIZED FIELDS ==-- //
            [SerializeField] List<DayController> m_dayControllers;
            [SerializeField] StampAreaController m_stampArea;

            [SerializeField] PageArrowController m_leftArrow;
            [SerializeField] PageArrowController m_rightArrow;
            [SerializeField] EndDayButtonController m_endDayButton;

            [SerializeField] DocumentController m_document;
        // ==--


        // --== CLASS FIELDS ==-- //
            private List< KeyValuePair<DayController, List<DocumentData>> > m_loadedDays;
            private uint m_currentDay = 0;
            private uint m_currentDocument = 0;

            /// <summary>
            /// The key of this dictionary is the type of a class implementing IPowerPlant
            /// </summary>
            private Dictionary<Type, List<IPowerPlant>> m_powerPlants;
        // ==--


        // --== ATTACHED COMPONENTS ==-- //
        // ==--


        // --== CLASS METHODS ==-- //
        // ==--


        // --== UNITY METHODS ==-- //
            void Start()
            {
                //
            }
        // ==--
    }
}
