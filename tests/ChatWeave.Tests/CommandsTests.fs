namespace ModelWeave.Commands.Tests

open System
open Xunit
open ChatWeave.Common.Process

module AddCommandTests =
    let chatweaveProjectFile = @"..\\..\\..\\..\\..\\src\\ChatWeave\\ChatWeave.fsproj"

    [<Fact>]
    let ``Add command accepts text and tags and exits with code 0`` () =
        let args = "add -t \"Hydration reminder\" -g health,wellness"
        let stdout, stderr, exitCode = runCli chatweaveProjectFile args

        Assert.Equal(0, exitCode)
        Assert.Contains("Adding memory", stdout)
        Assert.Contains("Hydration reminder", stdout)
        Assert.DoesNotContain("error", stderr.ToLower())
