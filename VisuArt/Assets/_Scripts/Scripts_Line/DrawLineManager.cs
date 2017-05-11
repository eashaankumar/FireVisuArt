using UnityEngine;
using System.Collections;

public class DrawLineManager : MonoBehaviour {
	//public SteamVR_TrackedObject trackedObj;
	private Controller rightTouch;
	public MeshLineRenderer currLine;
	public Material samplingMat;
	public Material cloneMat;
	private int numClicks = 0;

	// Update is called once per frame
	void Update () {		
        if(rightTouch == null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("RightTouch");
            if(temp != null)
            {
                rightTouch = temp.GetComponent<Controller>();
            }
        }
		//SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
		//if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
		if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)){
			GameObject go = new GameObject ("DrawingLine");
			go.AddComponent<MeshFilter> ();
			go.AddComponent<MeshRenderer> ();
			go.AddComponent<MeshLineRenderer> ();
			currLine = go.GetComponent<MeshLineRenderer> ();
			currLine.setWidth (.1f);
			currLine.lmat = new Material (cloneMat);
			currLine.lmat.color = samplingMat.color;
		
			numClicks = 0;
		} //else if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
		else if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger)){
			//currLine.setVertexCount (numClicks + 1);
			//currLine.SetPosition (numClicks, trackedObj.transform.position);
			currLine.AddPoint (rightTouch.transform.position);
			numClicks++;
		} else if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) {
			numClicks = 0;
			currLine = null;
		}

		if (currLine != null){
			//currLine.lmat.color = ColorManager.Instance.GetCurrentColor ();
			//currLine.lmat.color = Random.ColorHSV();
		}

}
}