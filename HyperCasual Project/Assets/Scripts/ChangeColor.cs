using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public GameObject body;
    public ParticleSystem collision_particles;

    //Whenever a collision with a ball occurs
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            //Changes the color
            body.GetComponent<SkinnedMeshRenderer>().material = other.gameObject.GetComponent<MeshRenderer>().material;
            
            //Spawns particles
            var NewParticle = Instantiate(collision_particles, transform.position, Quaternion.identity);
            NewParticle.GetComponent<Renderer>().material = other.gameObject.GetComponent<MeshRenderer>().material;

            //Removes the ball
            Destroy(other.gameObject);
        }

        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            //The Score adds 1 point on ball collision
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 10);
            MenuManager.MenuManagerInstance.menuElement[1].GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();
        }
    }
}
