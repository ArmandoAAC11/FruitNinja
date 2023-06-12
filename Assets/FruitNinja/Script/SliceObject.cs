using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{

    public Transform startSlicePoint; // The tip of the katana
    public Transform endSlicePoint;   // The handle of the katana
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

    public Material crossSectionMaterial;
    public float cutForce = 2000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 slicingDirection = endSlicePoint.position - startSlicePoint.position;
        bool hasHit = Physics.Raycast(startSlicePoint.position, slicingDirection, out hit, slicingDirection.magnitude ,sliceableLayer);
        if (hasHit)
        {
            if (hit.transform.gameObject.layer == 7)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }else
            {
                Slice(hit.transform.gameObject, hit.point, velocityEstimator.GetVelocityEstimate());
            }
        }
    }

    // Function to cut fruits
    public void Slice(GameObject target, Vector3 planePosition, Vector3 slicerVelocity)
    {
        Vector3 slicingDirection = endSlicePoint.position - startSlicePoint.position;
        Vector3 planeNormal = Vector3.Cross(slicerVelocity, slicingDirection);
        
        SlicedHull hull = target.Slice(planePosition, planeNormal, crossSectionMaterial);

        if (hull != null)
        {
            Score.score++;

            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }
    }

    // We set up  the slice that we have cut
    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
        Destroy(slicedObject, 2.5f);
    }
}
