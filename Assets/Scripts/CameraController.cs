using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Target;

    [SerializeField]
    private Vector3 m_Offset;

    [SerializeField]LayerMask m_LayerMask;

    RaycastHit hit;
    Vector3 heading;

    int cameraDistance=25;

    float pitchRotation = 0;
    float yatchRotation = 0;

    private void Awake()
    {
        heading = m_Target.transform.position - this.transform.position;
    }

    void Update()
    {
        transform.LookAt(m_Target.transform.position+new Vector3(0,10,0));
    }

    private void FixedUpdate()
    {
        heading = m_Target.transform.position - this.transform.position;

        float cantidadGirox = Input.GetAxis("Mouse X");
        float cantidadGiroy = Input.GetAxis("Mouse Y");
        transform.position = m_Target.transform.position+m_Offset;

        pitchRotation += cantidadGirox * 200 * Time.fixedDeltaTime;
        yatchRotation += cantidadGiroy * 200 * Time.fixedDeltaTime;

        if (yatchRotation < -23)
            yatchRotation = -23;

        transform.rotation = Quaternion.Euler(yatchRotation, pitchRotation, 0);

        if (Physics.Raycast(transform.position, heading.normalized, out hit, cameraDistance))
        {
            transform.position -= transform.forward * hit.distance;
            print("choca");
        }
        else
        {
        transform.position -= transform.forward * cameraDistance;
        }
    }

}
