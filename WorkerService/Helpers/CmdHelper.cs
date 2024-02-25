using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Helpers
{
    public sealed class CmdHelper
    {

            private static readonly CmdHelper instance = new CmdHelper();
            private Process cmdProcess;

            private CmdHelper()
            {
                StartCmdWithCommand("echo CmdHelper is initialized!");
            }

            public static CmdHelper Instance
            {
                get { return instance; }
            }

            public void ExecuteCommand(string command)
            {
                StartCmdWithCommand(command);
            }

            public void CloseCmd()
            {
                try
                {
                    cmdProcess.StandardInput.Close();
                    cmdProcess.WaitForExit();
                    cmdProcess.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            private void StartCmdWithCommand(string command)
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WindowStyle = ProcessWindowStyle.Normal,
                    WorkingDirectory = Environment.CurrentDirectory
                };

                cmdProcess = new Process { StartInfo = psi };

                cmdProcess.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
                cmdProcess.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);

                cmdProcess.Start();
                cmdProcess.BeginOutputReadLine();
                cmdProcess.BeginErrorReadLine();

                using (StreamWriter sw = cmdProcess.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(command);
                    }
                }

            }
        }

    }   

