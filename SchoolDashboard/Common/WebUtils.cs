using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

public static class WebUtils
{
    public static void DownloadFile(string url, string filePath)
    {
        var args = "\"" + url + "\" --output \"" + filePath + "\"";

        using (var process = new Process())
        {
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "curl"; 
            process.StartInfo.Arguments = args;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_OutputDataReceived;
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();
            if (process.ExitCode != 0)
                throw new Exception("File" + url + " now downloaded");
        }
    }

    private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        //Console.WriteLine(e.Data);
    }
}
