namespace ChatWeave.Commands

open System.CommandLine

module AddCommand =
    // This command creates a memory entry.
    // We're using named options so users can specify fields in any order.
    let textOpt = Option<string>("--text", "-t")
    textOpt.Description <- "Text to store"
    textOpt.HelpName <- "TEXT"

    let tagsOpt = Option<string[]>("--tags", "-g")
    tagsOpt.Description <- "Optional tags for classification"
    tagsOpt.HelpName <- "TAG"

    let command = Command("add", "Add a new memory")
    do
        command.Options.Add(textOpt)
        command.Options.Add(tagsOpt)

        command.SetAction(fun parseResult ->
            let text = parseResult.GetValue(textOpt)
            let tags = parseResult.GetValue(tagsOpt)
            printfn $"Adding memory: %s{text}"
            printfn $"With tags: %A{tags}"
            0)
