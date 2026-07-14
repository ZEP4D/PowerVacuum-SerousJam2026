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
            private List<IDocumentProposition> m_loadedPropositions;
            private int m_currentDay = 0;
            private int m_currentDocument = 0;

            /// <summary>
            /// The key of this dictionary is the type of a class implementing IPowerPlant
            /// </summary>
            private Dictionary<Type, List<IPowerPlant>> m_powerPlants;
            private List<IPowerPlant> m_incompletePowerPlants;
        // ==--


        // --== ATTACHED COMPONENTS ==-- //
        // ==--


        // --== CLASS METHODS ==-- //
            void PrepareForDay()
            {
                if( this.m_dayControllers.Count < this.m_currentDay ) throw new Exception(
                    "Trying to prepare for day nr " +
                    this.m_currentDay +
                    ", despite only having " +
                    this.m_dayControllers.Count +
                    " day controllers. (Indexing from zero)"
                );

                this.m_loadedPropositions.Clear();

                this.m_loadedPropositions.AddRange(
                    this.m_dayControllers[this.m_currentDay].GetDayPropositions(this as IMCP)
                );

                foreach( IPowerPlant incompletePlant in this.m_incompletePowerPlants )
                for( int idx = 0; idx < this.m_incompletePowerPlants.Count; idx++ )
                {
                    if( this.m_incompletePowerPlants[idx].GetState() == PowerPlantState.Complete )
                    {
                        this.m_incompletePowerPlants.RemoveAt(idx);
                        continue;
                    }
                    #nullable enable
                    // Should theoretically always return one, but just to be sure.
                    // Plus, if we ever want to add delays to build time, this will just work.
                    IDocumentProposition? buildStepProposition = this.m_incompletePowerPlants[idx].GetBuildProposition();
                    if( buildStepProposition is not null ) this.m_loadedPropositions.Add( buildStepProposition ); 
                }

            }
        // ==--


        // --== UNITY METHODS ==-- //
            void Start()
            {
                // Verify that supplied day controllers implement 'IDayController'
                
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
