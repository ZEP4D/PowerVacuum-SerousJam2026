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
            private List<IDayController> m_dayControllers = new();

            [field: Header("Scene objects")]
            [SerializeField] DocumentController m_document;
            [SerializeField] EndDayButtonController m_endDayButton;
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
                // Verify supplied day controllers implement 'IDayController'
                
                #nullable enable
                int idx = 0;
                foreach( GameObject candidateObject in this._dayControllers )
                {
                    IDayController? dayControlScript = candidateObject.GetComponent<IDayController>();

                    if( dayControlScript is not null ) this.m_dayControllers.Add( dayControlScript );
                    else Debug.LogError(
                        "Invalid Day Controller at index " +
                        idx +
                        ": '" +
                        candidateObject.name +
                        "'. Does not implement the 'IDayController' interface! Skipping..."
                    );

                    idx++;
                }
            }
        // ==--
    }
}
