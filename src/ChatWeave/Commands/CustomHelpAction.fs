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
                let usageHint =
                    match sub.Name with
                    | "add" -> "-t <TEXT> -g <TAG>"
                    | "recall" -> "<ID>"
                    | _ -> ""

                let padded = $"{sub.Name,-12}  {sub.Description}"
                let line = if usageHint <> "" then $"{padded}  {usageHint}" else padded
                out.WriteLine($"  {line}")
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
                let example =
                    match cmd.Name with
                    | "add" -> $"{root.Name} add -t \"Schedule sync-up with Kevin\" -g planning,team"
                    | "recall" -> $"{root.Name} recall 1234abc"
                    | "list" -> $"{root.Name} list"
                    | _ -> $"{root.Name} {cmd.Name}"

                out.WriteLine($"  {example}")
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
