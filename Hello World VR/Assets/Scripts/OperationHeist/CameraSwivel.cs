using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwivel : MonoBehaviour
{
    //start and end rotations
    public Transform startRotation;
    public Transform endRotation;
    private bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canRotate)
        {
            StartCoroutine(Swivel());
            canRotate = false;
        }
    }

    public IEnumerator Swivel()
    {
        for (float i = 0; i < 1.0f; i += Time.deltaTime/4)
        {
            //lerp camera rotation
            transform.rotation = Quaternion.Lerp(startRotation.rotation, endRotation.rotation, i);
            yield return null;
        }

        //switch start and end rotation
        Transform tempRotation = startRotation;
        startRotation = endRotation;
        endRotation = tempRotation;

        //stop camera from rotating for a second
        yield return new WaitForSeconds(1.0f);

        //set rotation bool
        canRotate = true;
    }
}
