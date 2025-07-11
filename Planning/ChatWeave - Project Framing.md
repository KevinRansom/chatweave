# Project Framing Document: MemoryTool  
**Prepared July 9, 2025**  
**Author: Kevin**  
**Contributor: Copilot (architectural critic, principle tester)**

---

## Problem Statement

Local LLMs often operate in constrained, stateless environments. Because of tight context windows and lack of persistence, they struggle to maintain coherence across extended interactions. This forces users to manually restate intent, retrace history, and manage context—all of which undermines agent utility and erodes the illusion of cognition.

---

## Motivation

While working with Gemma3B, a casual prompt exchange broke down when the model requested prior context—despite the information being logically recent. That moment exposed how fragile even short-range semantic memory can be. Rather than accept this as a hard limitation, this project explores how deliberate, inspectable memory scaffolding might restore continuity and control to local agents—without compromising user autonomy or privacy.

This is also a space for learning: a vehicle to understand how LLMs handle continuity, where memory fails, and how prompt-driven architecture can evolve to meet user needs.

---

## Design Values

| Principle                   | Description                                                                 |
|----------------------------|-----------------------------------------------------------------------------|
| **User Autonomy**          | Memory is deliberately curated—not passively accumulated.                  |
| **Privacy by Default**     | No transcript hoarding, background retention, or surveillance logic.       |
| **Transparency**           | Every memory artifact is inspectable, editable, and user-driven.           |
| **Runner & Model Independence** | No reliance on forking runners or privileged model access—prompt-based maintenance wherever possible. |
| **Prompt-Driven Protocols**| All memory repair, recap, and summarization mechanisms work via prompts.   |
| **Open-Source Collaboration**| Designed as a shared research clearing—not a universal standard.         |

---

## Platform Constraints

- Target implementation on **.NET**, with cross-platform flexibility.
- Must operate across varied runners (e.g., Ollama, LM Studio) with zero forking.
- Assumes lightweight model support—no privileged introspection or embedded memory layers.
- Features must degrade gracefully when environmental features are missing (e.g., no UI controls or system hooks).

---

## Open Source Philosophy

MemoryTool is an **open-source experimental platform** for agentic memory design. It aims to convene a like-minded community of privacy-first developers and researchers who:

- Explore principled tradeoffs in memory alignment  
- Prototype prompt-compatible memory logic for local models  
- Exchange ideas on drift mitigation, semantic repair, and audit-friendly design  

It’s not a canonical framework—rather, a **place where agent memory ideation can thrive**, free from vendor lock-in and surveillance defaults. The goal is cultural and technical: enabling experimentation that respects autonomy, invites critique, and scales thoughtfully with community insight.

---

## Project Goals & Status

| Objective                                  | Status             |
|-------------------------------------------|--------------------|
| Understand memory drift in local models   | In progress        |
| Prototype SummaryLedger & Recap protocols | Drafted            |
| Evaluate merge heuristics & decay logic   | Stress testing     |
| Preserve runner/model independence        | Actively challenged|
| Launch OSS infrastructure & community docs| Pending            |

---

## Design Tensions

Several features—e.g., contradiction alerts, relevance scoring—assume model introspection or host environment support. MemoryTool aims to **split capabilities into tiers**:

- **Core Features**: Prompt-based, universally portable  
- **Adaptive Features**: Enabled only if runner or model supports them  
- **Vendor-Aided Features**: Optional enhancements for platforms with deeper integration  

This design maintains clarity around what’s essential, what’s flexible, and what’s aspirational.

---

## Next Steps

1. Frame tiered capability matrix (core vs adaptive)  
2. Design MVP with recap + summary merge logic runnable in .NET  
3. Build public-facing repo and documentation scaffolding  
4. Invite pilot contributors for dry-run testing  
5. Maintain transparency in tradeoffs, limitations, and rationale  

---
