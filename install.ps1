# Obtener el directorio actual del script
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

# Variables de configuración
$dockerContainer = "postgres_container"
$dbUser = "user"
$dbName = "taskmaster-db"
$backupPath = Join-Path $scriptDir "backup.sql"
$containerBackupPath = "/var/lib/postgresql/data/backup.sql"
$dockerComposeFilePath = Join-Path $scriptDir "docker-compose.yml"
$serviceName = "db"  # Nombre del servicio definido en docker-compose.yml

# Realiza un backup
function Backup-Database {
    Write-Host "Realizando backup de la base de datos..."
    docker exec -t $dockerContainer pg_dump -U $dbUser $dbName > $backupPath
    Write-Host "Backup completado: $backupPath"
}

# Restaurar la base de datos desde un archivo de backup
function Restore-Database {
    Write-Host "Restaurando base de datos desde el backup..."

    # Copiar el archivo de backup al contenedor
    docker cp $backupPath "$dockerContainer`:$( $containerBackupPath )"
    
    # Leer el contenido del archivo SQL y pasarlo al comando de psql utilizando canalización en PowerShell
    $sqlCommands = Get-Content $backupPath -Raw
    docker exec -i $dockerContainer psql -U $dbUser -d $dbName -c "$sqlCommands"

    Write-Host "Restauración completada."
}

# Construir y desplegar servicio con Docker Compose
function BuildAndDeployService {
    Write-Host "Reconstruyendo y desplegando el servicio con Docker Compose..."
    docker-compose -f $dockerComposeFilePath up -d --no-deps --build
    Write-Host "Despliegue completado."
}

# Menú principal
function Show-Menu {
    param (
        [string]$Title = 'Gestión de Backup PostgreSQL en Docker'
    )
    Write-Host "==================== $Title ===================="
    Write-Host "1. Reconstruir y desplegar servicio"
    Write-Host "2. Realizar backup"
    Write-Host "3. Restaurar desde backup"
    Write-Host "4. Salir"
    Write-Host "================================================"

    $choice = Read-Host "Seleccione una opción (1-4)"
    switch ($choice) {
        '1' { BuildAndDeployService }
        '2' { Backup-Database }
        '3' { Restore-Database }
        '4' { Exit }
        default { Write-Host "Opción no válida, intente de nuevo." }
    }
}

# Ejecutar el menú en un bucle
while ($true) {
    Show-Menu
}