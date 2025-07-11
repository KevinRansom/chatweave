namespace ChatWeave.Commands

open System.CommandLine

module RecallCommand =
    // Retrieves a stored memory using its unique ID.
    // Using Argument for now, but may migrate to Option for flexibility.
    let idArg = Argument<string>("id")
    idArg.Description <- "The ID of the memory to recall"

    let command = Command("recall", "Recall a memory by ID")
    do
        command.Arguments.Add(idArg)

        command.SetAction(fun parseResult ->
            let id = parseResult.GetValue(idArg)
            printfn $"Recalling memory ID: %s{id}"
            0)
