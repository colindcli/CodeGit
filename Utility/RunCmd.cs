using System;
using System.Diagnostics;

public class RunCmd
{
    private readonly Process _process;
    public RunCmd()
    {
        _process = new Process();
    }

    public void Exe(string cmd)
    {
        _process.StartInfo.CreateNoWindow = true;
        _process.StartInfo.FileName = "cmd.exe";
        _process.StartInfo.UseShellExecute = false;
        _process.StartInfo.RedirectStandardInput = true;
        _process.StartInfo.RedirectStandardOutput = true;
        _process.StartInfo.RedirectStandardError = true;

        _process.OutputDataReceived += OutputDataReceived;
        _process.ErrorDataReceived += _proc_ErrorDataReceived;
        _process.Start();
        var cmdWriter = _process.StandardInput;
        _process.BeginOutputReadLine();
        if (!string.IsNullOrEmpty(cmd))
        {
            cmdWriter.WriteLine(cmd);
        }
        cmdWriter.Close();
        _process.Close();
    }

    private void _proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {

    }

    private static void OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            //Console.WriteLine(e.Data);
        }
    }
}
