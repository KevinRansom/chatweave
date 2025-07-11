namespace ChatWeave.Common

open System.Diagnostics

module Process =

    let runCli filename args =
        let psi = ProcessStartInfo()
        psi.FileName <- filename
        psi.Arguments <- args
        psi.RedirectStandardOutput <- true
        psi.RedirectStandardError <- true
        psi.UseShellExecute <- false

        use proc = new Process()
        proc.StartInfo <- psi
        proc.Start() |> ignore

        let output = proc.StandardOutput.ReadToEnd()
        let error = proc.StandardError.ReadToEnd()

        proc.WaitForExit()
        output, error, proc.ExitCode


