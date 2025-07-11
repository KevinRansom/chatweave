namespace ChatWeave.Commands

open System
open System.CommandLine
open System.CommandLine.Help
open System.CommandLine.Invocation

type CustomHelpAction(defaultHelp: HelpAction) =
    inherit SynchronousCommandLineAction()

    override _.Invoke(parseResult: ParseResult) =
        let root = parseResult.CommandResult.Command :?> RootCommand
        let out = Console.Out

        out.WriteLine("ChatWeave CLI — Command Reference\n")
        out.WriteLine($"Usage: {root.Name} [command] [options]\n")
        out.WriteLine($"Description: {root.Description}\n")

        if not (Seq.isEmpty root.Subcommands) then
            out.WriteLine("Available Commands:")
            for sub in root.Subcommands do
                out.WriteLine($"  {sub.Name,-12}  {sub.Description}")
            out.WriteLine()

        if not (Seq.isEmpty root.Options) then
            out.WriteLine("Global Options:")
            for opt in root.Options do
                let aliases = String.Join(", ", opt.Aliases)
                out.WriteLine($"  {aliases,-20}  {opt.Description}")
            out.WriteLine()

        if not (Seq.isEmpty root.Subcommands) then
            out.WriteLine("Examples:")

            for cmd in root.Subcommands do
                let cmdName = cmd.Name
                let opts =
                    cmd.Options
                    |> Seq.map (fun opt ->
                        let firstAlias =
                            opt.Aliases |> Seq.tryHead |> Option.defaultValue $"--{opt.Name}"
                        let placeholder =
                            if opt.HelpName <> null then $"<{opt.HelpName}>" else "<value>"
                        $"{firstAlias} {placeholder}")
                    |> String.concat " "

                out.WriteLine($"  {root.Name} {cmdName} {opts}")

            out.WriteLine()

        0


module HelpCommand =

    let attachCustomHelp (root: RootCommand) =
        for opt in root.Options do
            match opt with
            | :? HelpOption as help ->
                let defaultAction = help.Action :?> HelpAction
                help.Action <- CustomHelpAction(defaultAction)
            | _ -> ()
