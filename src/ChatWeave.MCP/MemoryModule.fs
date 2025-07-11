namespace ChatWeave.MCP

open System
open System.Threading
open System.Threading.Tasks
open ModelControl.Protocol

type MemoryModule() =
  interface IMcpModule with
    member _.Manifest =
      ModuleManifest.Load(".mcp/modules/com.chatweave.memory/chatweave-memory.module.json")

    member _.InvokeAsync(invocation, cancellationToken) =
      task {
        if invocation.Name <> "recall_memory" then
          return raise (InvalidOperationException($"Unsupported command: {invocation.Name}"))
        let query = invocation.Arguments.GetString("query")
        let! summary, confidence, refs = MemoryStore.FetchAsync(query, cancellationToken)
        return CommandResult(Output = {| summary = summary; confidence = confidence; source_refs = refs |})
      }
