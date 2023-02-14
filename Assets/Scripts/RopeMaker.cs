using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMaker : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private static GameObject preFabRope;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void makeRope(GameObject obj1, GameObject obj2)
    {
        float distance = Vector3.Distance(obj1.transform.localPosition, obj2.transform.localPosition);
        //GameObject GhostParent;
        float numOfBalls = distance / 0.1f;
        GameObject last_ball = obj1;
        for (int i = 1; i < numOfBalls; i++)
        {
            GameObject new_ball = Instantiate(Resources.Load("Link") as GameObject);
            new_ball.gameObject.transform.localPosition = Vector3.Slerp(obj1.transform.localPosition, obj2.transform.localPosition, ((float) i) / numOfBalls);
            last_ball.GetComponent<HingeJoint>().connectedBody = new_ball.GetComponent<Rigidbody>();
            last_ball = new_ball;
        }
        last_ball.GetComponent<HingeJoint>().connectedBody = obj2.GetComponent<Rigidbody>();
        //obj1.GetComponent<HingeJoint>().connectedBody = obj2.GetComponent<Rigidbody>();
        //obj2.GetComponent<HingeJoint>().connectedBody = obj1.GetComponent<Rigidbody>();
    }
}
