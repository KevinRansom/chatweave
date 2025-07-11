namespace ChatWeave.Commands

open System
open System.CommandLine

module AutoHelpName =
    let inferHelpName (opt: Option) =
        if String.IsNullOrWhiteSpace(opt.HelpName) then
            let baseName =
                if opt.Name.StartsWith("--") then opt.Name.Substring(2)
                elif opt.Name.StartsWith("-") then opt.Name.Substring(1)
                else opt.Name

            let cleanName = baseName.Replace("-", "").ToUpperInvariant()
            opt.HelpName <- cleanName