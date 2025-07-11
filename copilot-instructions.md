# ChatWeave `copilot-instructions.md`

This document steers GitHub Copilot toward generating code that reflects the architecture, ethics, and engineering standards of ChatWeave — a model-neutral, prompt-driven memory repair system for stateless LLMs.

## Collaboration Culture

We believe in:

- Memory as craft, not surveillance  
- Prompting as architecture, not workaround  
- Repair as co-authorship  
- AI as invited contributor, not lead architect  

This is a collaborative, AI-friendly workspace designed around user autonomy, philosophical clarity, and semantic intent.

## Product Standards

- ChatWeave CLI Tool  
  - Published as a **.NET Global Tool**  
  - Packaged via NuGet with `PackAsTool=true` and appropriate `ToolCommand` metadata  
  - Executable via `chatweave` after `dotnet tool install`  
  - Must include help scaffolds, descriptive metadata, and MIT license

- Library Assemblies  
  - Packaged as standard **NuGet libraries**  
  - Target framework: **.NET 9.0** (`net9.0`)  
  - Used internally or externally by ChatWeave protocol stacks  
  - Remain model-neutral and stateless; do not embed runtime assumptions

- Namespaces  
  - Assembly name = root namespace  
  - For modules: `Namespace.Module = AssemblyName`  

- File/Directory Naming  
  - Use CamelCase consistently  

- Build Scripts  
  - `Build.cmd` and `Build.sh` must build `ChatWeave.sln`  
  - `Test.cmd` and `Test.sh` execute test projects  

- Formatting Enforcement  
  - Use Fantomas via test pass to verify compliance  
  - IDE formatting is not assumed or enforced  

- Copyright Banner  
  ```fsharp
  // Copyright © 2025 The ChatWeave Contributors
  ```

## Testing Conventions

- Framework: xUnit  
- Language: F#  
- Execution: Tests must pass via `Test.cmd` / `Test.sh`  
- Style:  
  - Favor property-based testing for recall heuristics  
  - Avoid imperative mocks or fragile fixtures  
  - Prefer semantic inspection over procedural state

## Context Engineering Guidelines

### Planning Discipline

- Always begin with a semantic plan or checklist before implementing logic  
- Prefer checkbox-style TODO lists to simulate in-progress scaffolding  
- Assistants may check off items as code is generated  
  ```fsharp
  // ? Recap Pipeline Progress
  // [x] Define anchor structure
  // [x] Inject recap seed
  // [ ] Generate merge scaffolding
  // [ ] Emit trace logic for drift
  ```
- Checklist items may include tags:
  ```fsharp
  // [ ] Validate braid continuity  // [RecapStage]
  // [ ] Confirm anchor restoration // [AnchorEcho]
  ```
- Human contributors may expand or revise checklist structure

### Memory Awareness

- Do not assume persistent memory  
- Use explicit summary objects, cue injection, and anchor tags  
- Avoid internal state assumptions; prompt scaffolding governs memory flow

### Semantic Anchors

- Annotate code and CLI output with inspection tags:  
  `[DriftDetected]`, `[MergedRecall]`, `[AnchorEcho]`, `[RollbackTriggered]`  
- Use tags to support repair and diagnostic stages

### Fallback Behavior

- On recall failure, emit `injectCue`  
- On merge conflict, reroute to prompt rebase or anchor restore  
- Avoid retry loops unless justified by semantic trace

### Testing Mandates

- Each functional unit must include at least one test  
- Include edge cases for anchor conflict, drift detection, merge fuzz  
- Suggested naming:
  ```fsharp
  AnchorRecall_ShouldPreserveFraming()
  MergePatch_ShouldApplyWithoutDrift()
  ```

### Collaboration Etiquette

- Favor additive scaffolding  
- Respect contributor logic and style  
- Include commentary zones for iterative repair and refinement

## Logging Discipline & Debug Mode

### Privacy-Safe Logging

- Do not log raw prompts, user input, or identifiers unless explicitly enabled  
- Use semantic tokens (`User#123`, `Session#CUE42`) when metadata is needed  
- Logging is ephemeral and scoped to diagnostic inspection only

### Debug Assertion

- Do not assume compiler flags like `#define DEBUG` or `#if DEBUG`  
- Debug mode must be asserted via prompt semantics or CLI flags:  
  - `[EnableDiagnostics=true]`  
  - `chatweave recap --trace-level verbose`  
- Verbose output only allowed when debugging is active:
  ```text
  [RecapMergeFailed] Merge conflicted with anchor frame
  [RollbackTriggered] Depth = 2
  ```

## Licensing & Conduct

- License: MIT  
- Code of Conduct: .NET Foundation Contributor Covenant  
  https://dotnetfoundation.org/about/policies/code-of-conduct  
- File-Level Copyright
  ```fsharp
  // Copyright © 2025 The ChatWeave Contributors
  ```
