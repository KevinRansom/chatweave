# Building Agent Memory Without Surveillance  
A Model-Agnostic Architecture for Ethical, Token-Efficient Agent Memory  

## Overview

We propose ChatWeave (formerly SummaryLedger), a model-agnostic memory layer that gives stateless large language models the illusion of long-term context through iterative summarization, declarative recaps, and drift-aware correction. Rather than storing full transcripts, ChatWeave maintains a single evolving summary, supplemented by short “recap” artifacts and anchored tags, all under user control.

## Key Components

### ChatWeave Memory Layer

- Single evolving summary  
  Stores a recursive compression of the conversation. Each turn ingests the prior summary, the user’s prompt, and the model’s response, then emits a new compressed summary within a configurable token budget.

- Token budget management  
  Enforces a fixed?size context window. Low-signal details decay unless reaffirmed by recaps or anchor tags.

- Editable, inspectable interface  
  Exposes the summary and recaps in a text or Markdown buffer. Users may prune, rephrase, tag segments as `essential`, `ephemeral`, or `instructional`, and roll back to prior snapshots.

### Recap Artifacts & Merging Protocol

- Declarative recaps  
  Short, human- or agent-authored notes about recent exchanges, categorized as `ephemeral`, `anchoring`, `fact-correction`, or `spec-note`.

- Summary ingestion and merge logic  
  On each update, ChatWeave merges:
  1. Prior summary  
  2. New recaps  
  3. Current prompt/response pair  
  The merge reinforces recurring points, resolves conflicts by prompting the user (e.g. “Previously you said X, but summary says Y. Confirm?”), and applies relevance scores.

- Decay model  
  Recaps and summary fragments decay by age, redundancy, or frequency gaps unless marked `persistent`. Time-tagged recaps enable audit trails and rollback.

### Drift Detection & Correction

- Drift types  
  - **Ephemeral hallucination**: momentary generation errors  
  - **Semantic drift**: gradual divergence from intended tone or scope  
  - **Misbinding**: incorrect fusion of roles or facts  

- Detection strategies  
  - Anchor tracing: embed and monitor conceptual anchors  
  - Declarative dissonance alerts: flag contradictions between output and known intents  
  - User-curated drift flags: allow tagging segments as `inaccurate` or `overfitted`

- Correction protocols  
  - Memory patching: user edits to prune or clarify summary  
  - Assertion injection: inject declarative cues (e.g. “Agent is not human”)  
  - Rollback commands: revert to known-good summary snapshots via `rollback_summary`

### MCP Module Interface

- Standardized module (`com.modelweave.memory`) compatible with Gemma’s `.mcp/modules` discovery  
- Command `recall_memory(query: string) ? { summary, confidence, source_refs }`  
- Host integration: discover module manifest, register tool, dispatch `tool_call`, append summary to assistant stream  

## Recursive Prompt Loop

1. User submits prompt  
2. Model generates response using (prompt + summary + optional recaps)  
3. Model or agent emits recap artifacts  
4. ChatWeave merges summary + recaps + prompt/response into a new summary  
5. CLI or MCP tool calls (e.g. `recall_memory`) may inject additional context  
6. Next turn proceeds with updated summary and any recalled excerpts  

## Shared Memory Interface: Sidebar Summaries

- Mutual artifact: summary and recaps visible to both user and agent  
- Session resumption: users resume or hand off without replaying every turn  
- Editable & exportable: UI supports live pruning, tagging, and export for documentation  
- Trust & transparency: audit-by-design, GDPR-aligned memory buffer  

## Why It’s Useful

- Extended effective context while maximizing token space  
- Natural memory decay unless content is reaffirmed  
- Reduced semantic drift through anchored recaps and alerts  
- User trust: memory is transparent, editable, and consent-driven  
- Auditable timelines via time-tagged recaps and rollback snapshots  

## Advantages Over Other Approaches

| Feature               | Full-History Memory             | ChatWeave Approach                                   |
|-----------------------|---------------------------------|------------------------------------------------------|
| Storage               | Immutable transcripts           | Lossy, evolving summaries + declarative recaps       |
| Scalability           | Context window limits           | Fixed token budget; recaps reinforce key points      |
| Transparency          | Hidden recall logic             | Live, user-editable summary and recap buffer         |
| Privacy control       | Vendor-controlled retention     | User-curated, self-pruning memory                    |
| Model dependency      | Custom memory modules           | Pure prompt chaining + MCP; fully model-agnostic     |

## Immediate Benefits for Local LLMs

- Lightweight prompt-chaining integration (e.g. Gemma 3-27B on Ollama)  
- CLI toolchain installable as a .NET global tool (`dotnet tool install chatweave`)  
- Token-efficient dialogue extension with semantic continuity  
- Structured recall via MCP commands (e.g. `recall_memory`)  

## Strategic Opportunity for Large LLM Platforms

- Ethical differentiation through consent-driven memory  
- Enterprise compliance: transparent audit trails and user agency  
- Cross-platform philosophy: works across models, providers, and hosts  
- Modular backends: pairs with vector search or audit logs without altering agent logic  

## Privacy and Regulatory Manifesto

### Principles

- User-curated memory: agents only remember what users choose to record  
- Contextual forgetting: details fade unless explicitly reaffirmed  
- GDPR alignment: supports inspect, delete, and minimize data rights  
- Auditability by design: fully inspectable, correctable memory artifacts  

### Context

Unlike proposals advocating indefinite transcript retention, ChatWeave mimics human memory decay and champions user consent, avoiding the surveillance aesthetic and fostering trust in personal and enterprise contexts.

## Conclusion

ChatWeave combines evolving summaries, declarative recaps, drift detection, and rollback tooling into an ethical, effective memory layer for stateless agents. It scales with conversation, preserves privacy, and invites user-guided cognition instead of passive data accumulation.
---

_Drafted by Kevin, annotated by Gemma, assembled by Copilot (Dood by nature, Copilot by name)._