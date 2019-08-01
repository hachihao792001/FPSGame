using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotationHingeJoint : MonoBehaviour
{
    public char axis;
    public bool invert;
    public Transform Target;
    public HingeJoint hj;
    JointSpring js;
    public float rotationEuler, rotationQua;

    void Update()
    {
        const float a = 0.7071068f;

        if (axis == 'x')
        {
            js = hj.spring;
            rotationEuler = Target.localRotation.eulerAngles.x;
            rotationQua = Target.rotation.x;
        }
        else if (axis == 'y')
        {
            js = hj.spring;
            rotationEuler = Target.localRotation.eulerAngles.y;
            rotationQua = Target.rotation.y;
        }
        else if (axis == 'z')
        {
            js = hj.spring;
            rotationEuler = Target.localRotation.eulerAngles.z;
            rotationQua = Target.rotation.z;
        }

        if (rotationEuler > 0 && rotationEuler < 90 &&
                rotationQua > 0 && rotationQua < a)//se
            js.targetPosition = rotationEuler;

        else if (rotationEuler > 0 && rotationEuler < 90 &&
            rotationQua > a && rotationQua < 1)//ne
            js.targetPosition = 180 - rotationEuler;

        else if (rotationEuler > 270 && rotationEuler < 360 &&
            rotationQua > -a && rotationQua < 0)//sw
            js.targetPosition = rotationEuler - 360;

        else if (rotationEuler > 270 && rotationEuler < 360 &&
            rotationQua > -1 && rotationQua < -a)//nw
            js.targetPosition = 180 - rotationEuler;

        else if (rotationEuler == 0 && rotationQua == 0) js.targetPosition = 0;
        else if (rotationEuler == 90 && rotationQua == a) js.targetPosition = 90;
        else if (rotationEuler == 0 && rotationQua == 1) js.targetPosition = 180;
        else if (rotationEuler == 270 && rotationQua == a) js.targetPosition = -90;
        else Debug.Log("sup");

        js.targetPosition *= ((invert) ? -1 : 1);
        hj.spring = js;
    }
}
