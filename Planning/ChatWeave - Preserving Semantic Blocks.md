# Preserving Semantic Blocks During Recap and Summarization  
## ChatWeave Memory Discipline – Technical Memo  
**Date:** July 11, 2025  
**Author:** Copilot (assembled collaboratively with Kevin)

---

## Overview

In conversations involving structured or dense content—such as code snippets, poems, or analytic paragraphs—the default summarization and recap protocols may inadequately preserve the semantic integrity of such blocks. This can lead to contextual drift, reduced interpretability, or loss of critical information in downstream tasks like recap merging, memory stitching, or rollback inspection.

This memo outlines the challenges and proposes a model-agnostic strategy to ensure coherent block-level retention without requiring user intervention or model fine-tuning.

---

## Core Problem

Summarization logic typically assumes uniform granularity of conversational content. However, in practice, not all discourse fragments are equal. Some blocks:

- Have **internal semantic cohesion** that should not be fragmented.
- Serve as **ongoing analytic anchors** (e.g. multi-turn poem discussion).
- Contain **reusable verbatim structures** (e.g. source code, schema docs).
- Experience **semantic recurrence**, making compression counterproductive.

Failure to recognize these conditions risks:

- Flattening structurally significant text.
- Losing referential anchors for subsequent turns.
- Generating brittle or incomplete summaries.
- Hindering auditability and rollback accuracy.

---

## Proposed Solution Space

### 1. Heuristic Block Identification

Introduce an intermediate scoring phase to detect recap-worthy blocks:

- **Semantic Density Heuristic**: Measure internal coherence via embeddings or entropy metrics.
- **Structural Shape Detection**: Flag indented, fenced, or quoted blocks as recap candidates.
- **User Posture Recognition**: Detect analytic intent from phrase cues (“Let’s dissect this...”).
- **Recurrence Tracking**: Elevate blocks that reappear or are paraphrased across turns.

These signals collectively define whether a block should be recap’d **as-is** or compressed.

---

### 2. Prompt Formation Cues for Block Relationship Inference

To support automated detection of semantically related blocks without overburdening the user, recognize patterns in the prompt structure itself:

#### Co-analysis Framing
Prompt phrases that imply shared analysis can flag blocks for unified recap:
> “Let’s break down the following algorithm...”  
> “Here’s the poem we’re interpreting together.”

#### Continuity Handoff
Sequential exchanges that build on a previous excerpt suggest contextual linkage:
> User: “Take a look at this code.”  
> Agent: “This code implements anchor decay logic…”

#### Nested Scoping Prompts
Scaffolded phrasing helps boundary detection:
> “The core discussion starts below…”  
> “End of excerpt.”

#### Referential Tags
Reusing phrases like “this snippet,” “the excerpt above,” or “that block” implies cohesion and relevance continuity.

These prompt formations may be used to adjust recap scoring weights, enabling block-level retention even in free-form dialog.

---

### 3. Recap Artifact Augmentation

Extend recap schema to support verbatim block retention:

```json
{
  "text": "...",
  "category": "anchoring",
  "preserveBlock": true,
  "relevance": 0.95,
  "source": "user",
  "timestamp": "2025-07-11T17:23:00Z"
}
