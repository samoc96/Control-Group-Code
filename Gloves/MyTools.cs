using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MyTools
{

    [MenuItem("My Tool/Reset Report: %F12")]
    static void DEV_ResetReport()
    {
        CSV_Manager.CreateReport();
        EditorApplication.Beep();
        Debug.Log("<color=green>The report has been reset...!</color>");
    }
}
