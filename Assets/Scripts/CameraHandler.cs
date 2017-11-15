using UnityEngine;
using UnityEngine.EventSystems;

public class CameraHandler : MonoBehaviour
{
   [SerializeField] private float speed = 20f;
	// Update is called once per frame
	private void Update () {
	    if (Input.anyKey)
	    {
            Vector3 rotation = new Vector3();
	        if (Input.GetKey(KeyCode.A))
	            rotation.y += 1f;
	        
            if(Input.GetKey(KeyCode.D))
                rotation.y -= 1f;

            if (Input.GetKey(KeyCode.W))
                rotation.x += 1f;

            if (Input.GetKey(KeyCode.S))
                rotation.x -= 1f;

            transform.Rotate(rotation,speed*Time.deltaTime,Space.Self);
        }
	    if (Input.touchCount > 0)
	    {
	        Touch touch = Input.GetTouch(0);
	        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
	        {
	            Vector2 rotation = new Vector2(-touch.deltaPosition.y, touch.deltaPosition.x);
	            transform.Rotate(rotation * Time.deltaTime * speed, Space.Self);
	        }
	    }
    }
}
