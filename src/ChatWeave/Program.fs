namespace ChatWeave

open System.CommandLine
open System.CommandLine.Invocation
open ChatWeave.Commands

module Main =
  [<EntryPoint>]
  let main argv =
      let root = RootCommand("ChatWeave CLI")

      root.Subcommands.Add(AddCommand.command)
      root.Subcommands.Add(RecallCommand.command)
      root.Subcommands.Add(ListCommand.command)

      let config = CommandLineConfiguration(root)
      let parseResult = root.Parse(argv, config)

      parseResult.Invoke()