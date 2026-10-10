#!/usr/bin/env bash
set -euo pipefail
mkdir -p TestResults
grid="specharbor-${BUILD_NUMBER:-local}-$$"
app_pid=''
cleanup() {
  docker logs "$grid" > TestResults/grid.log 2>&1 || true
  docker rm -f "$grid" >/dev/null 2>&1 || true
  if [ -n "$app_pid" ]; then kill "$app_pid" 2>/dev/null || true; fi
}
trap cleanup EXIT
python3 -m http.server 8080 --bind 127.0.0.1 --directory demo > TestResults/demo.log 2>&1 &
app_pid=$!
docker run -d --name "$grid" --network host --shm-size=2g -e SE_NODE_GRID_URL=http://localhost:4444 selenium/standalone-chrome:4.49.0-20260909
ready=false
for i in $(seq 1 60); do
  if curl -fsS http://localhost:4444/status | python3 -c 'import json,sys;sys.exit(0 if json.load(sys.stdin)["value"]["ready"] else 1)' && curl -fsS http://127.0.0.1:8080 >/dev/null; then ready=true; break; fi
  sleep 2
done
[ "$ready" = true ] || { echo 'Grid or demo failed to become ready'; exit 1; }
export BASE_URL=http://127.0.0.1:8080 SELENIUM_REMOTE_URL=http://localhost:4444 HEADLESS=true
dotnet restore tests/SpecHarbor.Tests --locked-mode
dotnet test tests/SpecHarbor.Tests --no-restore --configuration Release --logger "junit;LogFilePath=$(pwd)/TestResults/acceptance.xml" --logger trx --results-directory TestResults
