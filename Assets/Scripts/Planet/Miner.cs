using UnityEngine;

namespace PlanetManagement
{

    public class Miner : MonoBehaviour
    {
        private OreTypes oreType;
        private float orePerSecond;

        private void Initalize(OreTypes _oreType, float _orePerSecond)
        {
            oreType = _oreType;
            orePerSecond = _orePerSecond;
        }

        private void Update()
        {

        }
    }
}