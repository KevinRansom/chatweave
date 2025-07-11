#!/usr/bin/env bash
set -euo pipefail
dotnet build -restore ChatWeave.sln --configuration Release
