using UnityEngine;

namespace PlanetManagement
{
    public class ResourceCalculater : MonoBehaviour
    {
        [SerializeField] private Heatmap heatmap;
        [SerializeField] private TextSet textSet;

        [SerializeField] private GameObject prefab, CurrentPlaceble;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                CurrentPlaceble.SetActive(true);
                CurrentPlaceble.transform.position = hit.point;
                CurrentPlaceble.transform.LookAt(heatmap.transform.position);
                textSet.UpdateText(heatmap.GetIntesityAtPoint(hit.point), 3);
            }
            else
            {
                CurrentPlaceble.SetActive(false);
            }
        }

        private void OnEnable()
        {
            CurrentPlaceble = Instantiate(prefab, new Vector3(), Quaternion.identity);
            CurrentPlaceble.SetActive(false);
        }
    }
}
