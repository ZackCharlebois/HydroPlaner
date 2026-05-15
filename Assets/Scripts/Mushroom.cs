using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Mushroom : MonoBehaviour {

    private Material MaterialInstance;
    private Color OgColor;
    private SphereCollider colldier;

    private void Awake()
    {
        MaterialInstance = GetComponent<Renderer>().material;
        MaterialInstance.EnableKeyword("_EMISSION");
        OgColor = MaterialInstance.GetColor("_EmissionColor");
        colldier = GetComponent<SphereCollider>();
    }


    public void OnCollisionStay(Collider col){
        if (!col.CompareTag("Player")){ return; }


        MaterialInstance.SetColor("_EmissionColor", OgColor * Mathf.LinearToGammaSpace((transform.position - col.transform.position).magnitude));
    }

        
}
