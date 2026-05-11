//https://www.youtube.com/watch?v=YUWfHX_ZNCw The youtube video I used
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ParticlesController: MonoBehaviour{
    public Color paintColor;
    
    public float minRadius = 0.05f;
    public float maxRadius = 0.2f;
    public float strength = 1;
    public float hardness = 1;
    [Space]
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;

    void Start(){
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other) { //The comments are from yours truly, Riley 
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Paintable p = other.GetComponent<Paintable>(); //Gets the paintable script
        if(p != null){
            for  (int i = 0; i< numCollisionEvents; i++){
                Vector3 pos = collisionEvents[i].intersection;//Finds the vector3 of the UV
                float radius = Random.Range(minRadius, maxRadius); //Sets the radius of the circle
                PaintManager.instance.paint(p, pos, radius, hardness, strength, paintColor); //Calls the paint manager script

                
                StartCoroutine(destroyParticle(p, pos, radius, hardness, strength)); //This sets it back to the origianl mask
            }
        }


    }

    

    private IEnumerator destroyParticle(Paintable p, Vector3 pos, float radius, float hardness, float strength) //I added this part - Riley
    {
        Color color = new Color(0, 0, 0, 0); //sets the color to black and the alpha to draw over the mask again
        yield return new WaitForSeconds(3f);
        PaintManager.instance.paint(p, pos, radius, hardness, strength, color); 
    }
}