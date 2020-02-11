using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSV_Manager
{
    private static string reportDirectoryName = "Report";
    private static string reportFileName = "report.csv";
    private static string reportSeparator = ",";
    private static string[] reportHeaders = new string[] {
        "Time (ms)",
        "Index MCP BendAngle",
        "Index PIP BendAngle",
        "Index DIP BendAngle",
        "Index MCP SplitAngle",
        "Thumb MCP BendAngle",
        "Thumb PIP BendAngle",
        "Thumb DIP BendAngle",
        "Thumb MCP SplitAngle",
        "Index Force"


    };

    //private static string timeStampHeader = "time stamp";

    #region Interactions
    public static void AppendToReport(string[] strings)
    {
        VerifyDirectory();
        VerifyFile();
        using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < strings.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeparator;
                }
                finalString += strings[i];
            }
            //finalString += GetTimeStamp();
            sw.WriteLine(finalString);
        }

    }

    public static void CreateReport()
    {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < reportHeaders.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeparator;
                }
                finalString += reportHeaders[i];
            }
            //finalString += reportSeparator + timeStampHeader;
            sw.WriteLine(finalString);
        }
    }

    #endregion

    #region Operations

    static void VerifyDirectory()
    {
        string dir = GetDirectoryPath();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    static void VerifyFile()
    {
        string file = GetFilePath();
        if (!File.Exists(file))
        {
            CreateReport();
        }
    }

    #endregion

    #region Queries
    static string GetTimeStamp()
    {
        return System.DateTime.UtcNow.ToString();
    }
    static string GetDirectoryPath()
    {
        return Application.dataPath + "/" + reportDirectoryName;
    }

    static string GetFilePath()
    {
        return GetDirectoryPath() + "/" + reportFileName;
    }
    #endregion

}
