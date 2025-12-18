# ============================================
# TECHFUTURE CRM - SCRIPT DE EXECUÇÃO CORRETO
# ============================================

Write-Host "=== TECHFUTURE CRM - SA- 3 ===" -ForegroundColor Cyan
Write-Host "Sistema de Vendas e Atendimento" -ForegroundColor Yellow
Write-Host "Equipe 3" -ForegroundColor Yellow
Write-Host ""

# Restaurar Backend
Write-Host "1. Restaurando Backend..." -ForegroundColor Green
cd ".\backend"
dotnet restore

if ($?) {
    Write-Host "   ✅ Backend restaurado" -ForegroundColor Green
} else {
    Write-Host "   ❌ Erro no backend" -ForegroundColor Red
}

# Restaurar Frontend
Write-Host "2. Restaurando Frontend..." -ForegroundColor Green
cd "..\frontend"
dotnet restore

if ($?) {
    Write-Host "   ✅ Frontend restaurado" -ForegroundColor Green
} else {
    Write-Host "   ❌ Erro no frontend" -ForegroundColor Red
}

# Voltar para pasta principal
cd ".."

Write-Host ""
Write-Host "🎯 INSTRUÇÕES PARA EXECUTAR:" -ForegroundColor Cyan
Write-Host ""
Write-Host "PRIMEIRO: Terminal 1 - Backend" -ForegroundColor Yellow
Write-Host "   cd backend" -ForegroundColor White
Write-Host "   dotnet run" -ForegroundColor White
Write-Host ""
Write-Host "DEPOIS: Terminal 2 - Frontend" -ForegroundColor Yellow
Write-Host "   cd frontend" -ForegroundColor White
Write-Host "   dotnet run" -ForegroundColor White
Write-Host ""
Write-Host "🌐 API Backend: http://localhost:5000" -ForegroundColor Cyan
Write-Host "📊 Status: http://localhost:5000/api/status" -ForegroundColor Cyan
Write-Host ""
Write-Host "Pressione Enter para sair..." -ForegroundColor Gray
Read-Host
