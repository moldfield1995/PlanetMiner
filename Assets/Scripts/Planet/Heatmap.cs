using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace PlanetManagement
{

    public struct PlanetOreData
    {
        public Vector4[] Positions;
        public float[] Radiuses;
        public float[] Intensities;
    }

    public class Heatmap : MonoBehaviour
    {

        [SerializeField] private Material material;

        [SerializeField] //dose it need to be class wide or local?
        private int count = 10;

        [SerializeField] private GameObject prefab;

        private GameObject[] points; //for debuging
        private int currnetlySelectedOre = 0;
        private List<PlanetOreData> planetOreList;
        private PlanetOreData currentPlanetOreData;

        private float gloabalIntensity = 1f;

        //Shader Set ID's
        private int shaderPoints, shaderLenght, shaderRadius, shaderIntensity, shaderGlobal;

        private void Awake()
        {
            points = new GameObject[count]; //Debug
            shaderGlobal = Shader.PropertyToID("_globalIntensity");
            shaderIntensity = Shader.PropertyToID("_intensity");
            shaderRadius = Shader.PropertyToID("_radius");
            shaderLenght = Shader.PropertyToID("_Points_Length");
            shaderPoints = Shader.PropertyToID("_Points");
            planetOreList = new List<PlanetOreData>(5);
        }

        private void Start()
        {
            Vector4[] positions = new Vector4[count];
            float[] radiuses = new float[count];
            float[] intensities = new float[count];
            float toRadients = Mathf.PI / 180;

            //Area Gen completly random atm TODO:De-randomize
            float angle, height, radius = transform.localScale.x / 2;
            Vector3 randomVector;
            for (int i = 0; i < count; i++)
            {
                angle = (Random.Range(0f, 360f) * toRadients);
                height = (Random.Range(0f, 360f) * toRadients);
                //set to a point on a sphere using this speher
                randomVector = new Vector3(radius * Mathf.Cos(angle) * Mathf.Sin(height),
                    radius * Mathf.Sin(angle) * Mathf.Sin(height), radius * Mathf.Cos(height));
                points[i] = Instantiate(prefab, randomVector, Quaternion.identity, transform);
                points[i].name = i.ToString();
                positions[i] = randomVector;
                radiuses[i] = Random.Range(.7f, 10f);
                intensities[i] = Random.Range(0f, 1f);
            }
            Assert.IsTrue(count <= 20);
            material.SetInt(shaderLenght, count);
            material.SetVectorArray(shaderPoints, positions);
            material.SetFloatArray(shaderRadius, radiuses);
            material.SetFloatArray(shaderIntensity, intensities);
            material.SetFloat(shaderGlobal, gloabalIntensity);

            currentPlanetOreData = new PlanetOreData
            {
                Intensities = intensities,
                Positions = positions,
                Radiuses = radiuses
            };
            planetOreList.Add(currentPlanetOreData);
        }

        public void GenerateNewOre(int nodeCount = 10)
        {
            count = nodeCount;
            Vector4[] positions = new Vector4[count];
            float[] radiuses = new float[count];
            float[] intensities = new float[count];
            float toRadients = Mathf.PI / 180;

            //Area Gen completly random atm TODO:De-randomize
            float angle, height, radius = transform.localScale.x / 2;
            Vector3 randomVector;
            for (int i = 0; i < count; i++)
            {
                angle = (Random.Range(0f, 360f) * toRadients);
                height = (Random.Range(0f, 360f) * toRadients);
                //set to a point on a sphere using this speher
                randomVector = new Vector3(radius * Mathf.Cos(angle) * Mathf.Sin(height),
                    radius * Mathf.Sin(angle) * Mathf.Sin(height), radius * Mathf.Cos(height));
                points[i].transform.position = randomVector;
                positions[i] = randomVector;
                radiuses[i] = Random.Range(.7f, 10f);
                intensities[i] = Random.Range(0f, 1f);
            }
            Assert.IsTrue(count <= 20);
            material.SetInt(shaderLenght, count);
            material.SetVectorArray(shaderPoints, positions);
            material.SetFloatArray(shaderRadius, radiuses);
            material.SetFloatArray(shaderIntensity, intensities);
            currentPlanetOreData = new PlanetOreData
            {
                Intensities = intensities,
                Positions = positions,
                Radiuses = radiuses
            };
            planetOreList.Add(currentPlanetOreData);
        }

        public void SwtichOre(int newOre)
        {
            Assert.IsTrue(newOre < planetOreList.Count);
            currentPlanetOreData = planetOreList[newOre];
            material.SetInt(shaderLenght, currentPlanetOreData.Radiuses.Length - 1);
            material.SetVectorArray(shaderPoints, currentPlanetOreData.Positions);
            material.SetFloatArray(shaderRadius, currentPlanetOreData.Radiuses);
            material.SetFloatArray(shaderIntensity, currentPlanetOreData.Intensities);
        }

        public int GetCurrentlySelectedOre()
        {
            return currnetlySelectedOre;
        }

        public int GetAmountOfOres()
        {
            return planetOreList.Count;
        }

        public void SetIntensity(float intensity)
        {
            gloabalIntensity = intensity;
            material.SetFloat(shaderGlobal, gloabalIntensity);
        }

        public float GetIntesityAtPoint(Vector3 point)
        {
            float intesity = 0f;
            Vector4[] positions = currentPlanetOreData.Positions;
            float[] radiuses = currentPlanetOreData.Radiuses;
            float[] intensities = currentPlanetOreData.Intensities;
            for (int i = 0; i < count; i++)
            {
                float distence = Vector3.Distance(point, positions[i]);
                float hi = 1 - Mathf.Clamp01(distence / radiuses[i]);
                intesity += Mathf.Lerp(0f, intensities[i], hi);
            }
            return intesity;
        }

        public int[] GetInteractingNodes(Vector3 point)
        {
            var Nodes = new List<int>(4);
            Vector4[] positions = currentPlanetOreData.Positions;
            float[] radiuses = currentPlanetOreData.Radiuses;
            float[] intensities = currentPlanetOreData.Intensities;
            for (int i = 0; i < count; i++)
            {
                float distence = Vector3.Distance(point, positions[i]);
                float hi = 1 - Mathf.Clamp01(distence / radiuses[i]);
                if (Mathf.Lerp(0f, intensities[i], hi) > 0f)
                    Nodes.Add(i);
            }
            return Nodes.ToArray();
        }
    }
}