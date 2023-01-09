using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Transform player;
    private Vector3 startMousePos, startPlayerPos;
    private bool moveThePlayer;
    //Range is used to add a slider menu
    [Range (0f,1f)]public float maxSpeed;
    [Range (0f,1f)]public float camSpeed;
    [Range (0f,50f)]public float pathSpeed;
    private float velocity,camVelocity;
    private Camera mainCam;
    public Transform path;
    private Rigidbody rb;
    private Collider _collider;
    

    void Start()
    {
        player = transform;
        maxSpeed = 0.5f;
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        //Getting mouse position
        if (Input.GetMouseButtonDown(0) && MenuManager.MenuManagerInstance.GameState){
            moveThePlayer = true;
            Plane newPlane = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (newPlane.Raycast(ray,out var distance)){
                startMousePos = ray.GetPoint(distance);
                startPlayerPos = player.position;
            }
        }
        else if (Input.GetMouseButtonUp(0)){
            moveThePlayer = false;
    }

        //Updating mouse position
    if (moveThePlayer){
        Plane newPlane = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (newPlane.Raycast(ray,out var distance)){
                Vector3 mouseNewPos = ray.GetPoint(distance);
                Vector3 MouseNewPos = mouseNewPos - startMousePos;
                Vector3 desiredPlayerPos = MouseNewPos + startPlayerPos;

        //Moving the player towards mouse position
            desiredPlayerPos.x = Mathf.Clamp(desiredPlayerPos.x, -1.6f, 1.6f);

        //Setting movement properties: speed & velocity
            player.position = new Vector3(Mathf.SmoothDamp(player.position.x, desiredPlayerPos.x, ref velocity, maxSpeed)
            ,player.position.y, player.position.z);
    }
}
        //Moving the level under player's feet
    if (MenuManager.MenuManagerInstance.GameState)
    {
    var pathNewPos = path.position;
    path.position = new Vector3(pathNewPos.x, pathNewPos.y, Mathf.MoveTowards(pathNewPos.z, -1000f, pathSpeed * Time.deltaTime));
}



}
        //Moving the camera with the player's movement smoothly
    private void LateUpdate()
    {
        var cameraNewPos = mainCam.transform.position;
        mainCam.transform.position = new Vector3(Mathf.SmoothDamp(cameraNewPos.x, player.transform.position.x, ref camVelocity, camSpeed), cameraNewPos.y, cameraNewPos.z);
    }


        //Ending level on colliding with obstacle
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            MenuManager.MenuManagerInstance.GameState = false;

        }
    }

    //Making the player jump on path's end
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("path"))
        {
            rb.isKinematic = _collider.isTrigger = false;
            rb.velocity = new Vector3(0f, 8f, 0f) ;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        rb.isKinematic = _collider.isTrigger = true;
    }

}