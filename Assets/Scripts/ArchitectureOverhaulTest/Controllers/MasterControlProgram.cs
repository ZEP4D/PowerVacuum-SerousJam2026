using UnityEngine;
using UnityEngine.Scripting;

using System;
using System.Collections.Generic;


using ArchitectureOverhaul.Common;
namespace ArchitectureOverhaul
{
    public class MasterControlProgram : MonoBehaviour
    {
        // --== SERIALIZED FIELDS ==-- //
            [SerializeField] List<GameObject> _dayControllers;
            [SerializeField] GameObject _documentStampArea;

            [SerializeField] PageArrowController m_leftArrow;
            [SerializeField] PageArrowController m_rightArrow;
            [SerializeField] EndDayButtonController m_endDayButton;

            [SerializeField] DocumentController m_document;
        // ==--


        // --== CLASS FIELDS ==-- //
            private List< KeyValuePair<IDayController, List<DocumentData>> > m_loadedDays;
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
