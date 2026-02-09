# Script PowerShell pour arrêter l'API COService
# Usage: .\stop-api.ps1

$port = 8700

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Arrêt de l'API COService" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 1. Recherche et arrêt des processus utilisant le port 8700
Write-Host "[1/2] Recherche des processus utilisant le port $port..." -ForegroundColor Yellow
$connections = Get-NetTCPConnection -LocalPort $port -ErrorAction SilentlyContinue
if ($connections) {
    foreach ($conn in $connections) {
        $processId = $conn.OwningProcess
        $process = Get-Process -Id $processId -ErrorAction SilentlyContinue
        if ($process) {
            Write-Host "  → Arrêt de $($process.ProcessName) (PID: $processId)" -ForegroundColor Red
            Stop-Process -Id $processId -Force -ErrorAction SilentlyContinue
        }
    }
} else {
    Write-Host "  ✓ Aucun processus trouvé sur le port $port" -ForegroundColor Green
}

# 2. Arrêt de tous les processus COService.API
Write-Host "[2/2] Arrêt de tous les processus COService.API..." -ForegroundColor Yellow
$apiProcesses = Get-Process -Name "COService.API" -ErrorAction SilentlyContinue
if ($apiProcesses) {
    foreach ($proc in $apiProcesses) {
        Write-Host "  → Arrêt de $($proc.ProcessName) (PID: $proc.Id)" -ForegroundColor Red
        Stop-Process -Id $proc.Id -Force
    }
    Write-Host "  ✓ Tous les processus ont été arrêtés" -ForegroundColor Green
} else {
    Write-Host "  ✓ Aucun processus COService.API trouvé" -ForegroundColor Green
}

Write-Host ""
Write-Host "API arrêtée avec succès!" -ForegroundColor Green
