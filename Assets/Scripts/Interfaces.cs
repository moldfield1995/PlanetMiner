using UnityEngine;
using System.Collections.Generic;
namespace PlanetManagement {
    abstract class PlanetNodes : MonoBehaviour
    {
        protected float storageAmount;
        protected float currentStored;

        protected List<PlanetNodes> OutboundNodes;
        protected enum distrabutionType
        {
            FillFirst,
            RoundRobin,
            Random
        }

        /// <summary>
        /// called on the object moving the resources too
        /// </summary>
        /// <param name="oreType">The Ore type moved</param>
        /// <param name="amount">The amount to be moved</param>
        /// <returns>if the transfer was sucsessfull</returns>
        public abstract bool TransferResources(OreTypes oreType, float amount);
    }
}