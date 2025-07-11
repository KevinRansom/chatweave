namespace ChatWeave.Commands

open System.CommandLine

module ListCommand =
    // Lists all stored memories.
    // Consider adding filters like --tag or --date in future.
    let command = Command("list", "List all memories")

    do
        command.SetAction(fun _ ->
            printfn "Listing all stored memories..."
            0)
