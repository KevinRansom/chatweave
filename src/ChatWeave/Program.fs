namespace ChatWeave

open System.CommandLine
open System.CommandLine.Help
open System.CommandLine.Invocation
open ChatWeave.Commands.AutoHelpName
open ChatWeave.Commands


module Main =

    [<EntryPoint>]
    let main argv =
        let root = RootCommand("ChatWeave CLI")

        root.Subcommands.Add(AddCommand.command)
        root.Subcommands.Add(RecallCommand.command)
        root.Subcommands.Add(ListCommand.command)

        HelpCommand.attachCustomHelp root

        for opt in root.Options do
            inferHelpName opt

        for cmd in root.Subcommands do
            for opt in cmd.Options do
                inferHelpName opt

        let config = CommandLineConfiguration(root)
        let parseResult = root.Parse(argv, config)

        parseResult.Invoke()