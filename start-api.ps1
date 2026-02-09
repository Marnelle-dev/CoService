# Script PowerShell pour démarrer l'API COService
# Usage: .\start-api.ps1

$port = 8700
$apiPath = "G:\PROJET 2025\REFONTE SEG\CO\COService.API"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Démarrage de l'API COService" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 1. Recherche et arrêt des processus utilisant le port 8700
Write-Host "[1/4] Recherche des processus utilisant le port $port..." -ForegroundColor Yellow
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
Write-Host "[2/4] Arrêt de tous les processus COService.API..." -ForegroundColor Yellow
$apiProcesses = Get-Process -Name "COService.API" -ErrorAction SilentlyContinue
if ($apiProcesses) {
    foreach ($proc in $apiProcesses) {
        Write-Host "  → Arrêt de $($proc.ProcessName) (PID: $proc.Id)" -ForegroundColor Red
        Stop-Process -Id $proc.Id -Force
    }
} else {
    Write-Host "  ✓ Aucun processus COService.API trouvé" -ForegroundColor Green
}

# 3. Attente pour libérer les fichiers
Write-Host "[3/4] Attente de la libération des fichiers..." -ForegroundColor Yellow
Start-Sleep -Seconds 3
Write-Host "  ✓ Fichiers libérés" -ForegroundColor Green

# 4. Vérification finale du port
Write-Host "[4/4] Vérification finale du port $port..." -ForegroundColor Yellow
$finalCheck = Get-NetTCPConnection -LocalPort $port -ErrorAction SilentlyContinue
if ($finalCheck) {
    Write-Host "  ⚠️  Le port $port est toujours utilisé!" -ForegroundColor Red
    Write-Host "  Veuillez arrêter manuellement le processus utilisant ce port." -ForegroundColor Red
    exit 1
} else {
    Write-Host "  ✓ Le port $port est libre" -ForegroundColor Green
}

Write-Host ""
Write-Host "Démarrage de l'API..." -ForegroundColor Cyan
Write-Host ""

# 5. Démarrage de l'API
Set-Location $apiPath
dotnet run
