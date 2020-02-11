using Libdexmo.Unity.Core.HandController;
using Libdexmo.Unity.Core.Model;
using Libdexmo.Unity.Core.Model.Calibration;
using Libdexmo.Unity.HandController;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class GetHandData : MonoBehaviour
{
    private bool isReady;
    private DexmoController controller;
    private UnityHandController leftHand;
    private UnityHandController rightHand;

    private CalibrationProfileManager profileMgr;
    private UnityHandRepresentation leftRep;
    private UnityHandRepresentation rightRep;
    private float timeTracker = 0;
    private string[] rowDataTemp;

    void Update()
    {
        FindController();
        if (isReady)
        {
            //Test();
            CSV_Manager.AppendToReport(GetReportLine());
        }
    }
    string[] GetReportLine()
    {
        string[] returnable = new string[10];
        returnable = DataTest();
        return returnable;
    }



    private void FindController()
    {
        if (isReady) return;
        controller = DexmoController.Instance;
        profileMgr = CalibrationProfileManager.Instance;

        if (controller != null && profileMgr != null)
        {
            isReady = true;
            leftHand = controller.HandControllerPairs[0].Left;
            rightHand = controller.HandControllerPairs[0].Right;
            leftRep = controller.HandPool.HandPairs[0].Left;
            rightRep = controller.HandPool.HandPairs[0].Right;
        }
    }

    /// <summary>
    /// GetJointBendAngle
    /// </summary>
    /// <param name="finger">0~4  Thumb~Pinky </param>
    /// <param name="joint"> 0~2（MCP、PIP and DIP）</param>
    /// <returns></returns>
    private float GetJointBendAngle(UnityHandController hand, UnityHandRepresentation rep, int finger, int joint)
    {
        if (isReady)
        {
            var value = hand.GetCurrentFingerRotationInfo().Fingers[finger].Bend.Value;
            var handProfile = profileMgr.FindProfile(rep.GraphicsHandModel.Hand);
            if (handProfile != null)
            {
                var angle = handProfile.Fingers[finger].Joints[joint].BendAngleExtreme * value;
                return angle;
            }
        }
        return 0;
    }
    private float GetJointSplitAngle(UnityHandController hand, UnityHandRepresentation rep, int finger, int joint)
    {
        if (isReady)
        {
            var value = hand.GetCurrentFingerRotationInfo().Fingers[finger].Split.Value;
            var handProfile = profileMgr.FindProfile(rep.GraphicsHandModel.Hand);
            if (handProfile != null)
            {
                var angle = handProfile.Fingers[finger].Joints[joint].SplitAngleExtreme * value;
                return angle;
            }
        }
        return 0;
    }

    /* private float GetJointRotationAngle(UnityHandController hand, UnityHandRepresentation rep, int finger, int joint)
    {
        if (isReady)
        {
            var value = hand.GetCurrentFingerRotationInfo().Thumb.Rotate.Value;
            var handProfile = profileMgr.FindProfile(rep.GraphicsHandModel.Hand);
            if (handProfile != null)
            {
                //                var angle = handProfile.Fingers[finger].Joints[joint]. value;\
                return 8;
            }
        }
        return 0;
    }*/

    private float GetForceIndex(UnityHandController hand, UnityHandRepresentation rep)
    {
        if (isReady)
        {

            var handProfile = profileMgr.FindProfile(rep.GraphicsHandModel.Hand);
            //float currentForce = 0;
            if (handProfile != null)
            {
                var value = hand.Index.GetCollisionInfoOnTouch().FingerImpControlInfo.Stiffness;
                //var value = hand.GetCollisionInfoOnTouch().FingerImpControlInfo.Stiffness;
                return value;
            }
        }
        return 0;
    }
    
    public string[] DataTest()
    {
        var bendIndexMCPangle = GetJointBendAngle(leftHand, leftRep, 1, 0);
        var bendIndexPIPangle = GetJointBendAngle(leftHand, leftRep, 1, 1);
        var bendIndexDIPangle = GetJointBendAngle(leftHand, leftRep, 1, 2);

        var splitIndexMCPangle = GetJointSplitAngle(leftHand, leftRep, 1, 0);
        var splitIndexPIPangle = GetJointSplitAngle(leftHand, leftRep, 1, 1);
        var splitIndexDIPangle = GetJointSplitAngle(leftHand, leftRep, 1, 2);

        var bendThumbMCPangle = GetJointBendAngle(leftHand, leftRep, 0, 0);
        var bendThumbPIPangle = GetJointBendAngle(leftHand, leftRep, 0, 1);
        var bendThumbDIPangle = GetJointBendAngle(leftHand, leftRep, 0, 2);

        var splitThumbMCPangle = GetJointSplitAngle(leftHand, leftRep, 0, 0);
        var splitThumbPIPangle = GetJointSplitAngle(leftHand, leftRep, 0, 1);
        var splitThumbDIPangle = GetJointSplitAngle(leftHand, leftRep, 0, 2);

        var forceIndex = GetForceIndex(leftHand, leftRep);
        timeTracker = timeTracker + 1000 * Time.deltaTime;


        rowDataTemp = new string[10];
        rowDataTemp[0] = "" + timeTracker;
        rowDataTemp[1] = "" + bendIndexMCPangle;
        rowDataTemp[2] = "" + bendIndexPIPangle;
        rowDataTemp[3] = "" + bendIndexDIPangle;
        rowDataTemp[4] = "" + splitIndexMCPangle;
        
        rowDataTemp[5] = "" + bendThumbMCPangle;
        rowDataTemp[6] = "" + bendThumbPIPangle;
        rowDataTemp[7] = "" + bendThumbDIPangle;
        rowDataTemp[8] = "" + splitThumbMCPangle;

        rowDataTemp[9] = "" + forceIndex;

        return rowDataTemp;
    }

}
