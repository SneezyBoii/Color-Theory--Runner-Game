using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject body;
    public ParticleSystem collision_particles;
    // Start is called before the first frame update
    void Start()
    { 
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            body.GetComponent<SkinnedMeshRenderer>().material = other.gameObject.GetComponent<MeshRenderer>().material;
            var NewParticle = Instantiate(collision_particles, transform.position, Quaternion.identity);
            NewParticle.GetComponent<Renderer>().material = other.gameObject.GetComponent<MeshRenderer>().material;
        }

    }
}
