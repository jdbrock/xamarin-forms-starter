function Write-Color([String[]]$Text, [ConsoleColor[]]$Color = "White", [int]$StartTab = 0, [int] $LinesBefore = 0,[int] $LinesAfter = 0)
{
    $DefaultColor = $Color[0]
    
    if ($LinesBefore -ne 0)
    { 
        for ($i = 0; $i -lt $LinesBefore; $i++)
        {
            Write-Host "`n" -NoNewline
        }
    }

    if ($StartTab -ne 0)
    { 
        for ($i = 0; $i -lt $StartTab; $i++)
        {
            Write-Host "`t" -NoNewLine
        }
    }
    
    if ($Color.Count -ge $Text.Count)
    {
        for ($i = 0; $i -lt $Text.Length; $i++)
        {
            Write-Host $Text[$i] -ForegroundColor $Color[$i] -NoNewLine
        } 
    }
    else
    {
        for ($i = 0; $i -lt $Color.Length ; $i++)
        {
            Write-Host $Text[$i] -ForegroundColor $Color[$i] -NoNewLine
        }
        for ($i = $Color.Length; $i -lt $Text.Length; $i++)
        {
            Write-Host $Text[$i] -ForegroundColor $DefaultColor -NoNewLine
        }
    }
    
    Write-Host
    
    if ($LinesAfter -ne 0)
    {
        for ($i = 0; $i -lt $LinesAfter; $i++)
        {
            Write-Host "`n"
        }
    }
}

function Recursive-Directory-Rename([String]$project, [String]$path)
{
    $dir = New-Object -TypeName System.IO.DirectoryInfo -ArgumentList $path

    if (!$dir -Or !$dir.Name)
    {
        return;
    }

    $children = Get-ChildItem $dir *
    
    foreach ($child in $children)
    {
        if ($child -Is [System.IO.DirectoryInfo])
        {
            Recursive-Directory-Rename $project $child.FullName
        }
    }
    
    if (!$dir.Name.Contains("StarterKit"))
    {
        return;
    }
        
    $oldName = $dir.Name;
    $newName = $dir.Name.Replace("StarterKit", $project)
    
    Write-Color "Renaming ", $dir.FullName, " => ", $newName -Color White, Yellow, White, Red
    
    Rename-Item -Path $dir.FullName -NewName $newName
}

Write-Host

$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

Write-Color "What is the name of your project? Use a short camel-case name like MyApp" -Color Green

$project = Read-Host -Prompt 'Name'

Write-Host

Write-Color "This script will rewrite/rename files in the script directory: ", $scriptDir -Color Red, Magenta

$yn = Read-Host -Prompt 'Proceed? [Y/n]'

if (($yn -eq 'y') -Or ($yn -eq 'Y'))
{
    Write-Host

    # FIND/REPLACE
    Write-Color "Find/Replace StarterKit => ", $project -Color Green, Green;
    Write-Color "-----------------------------------------------------" -Color Green;

    $files = Get-ChildItem $scriptDir * -rec |
        ?{ $_.FullName -inotmatch "\\bin\\" } |
        ?{ $_.FullName -inotmatch "\\obj\\" } |
        ?{ $_.FullName -inotmatch "\\packages\\" }
        
    foreach ($file in $files)
    {
        if ($file.Name.StartsWith("."))
        {
            continue;
        }
            
        if ($file -is [System.IO.DirectoryInfo])
        {
            continue;
        }
        
        if (!$file.Name.EndsWith(".cs") -AND
            !$file.Name.EndsWith(".xml") -AND
            !$file.Name.EndsWith(".xaml") -AND
            !$file.Name.EndsWith(".csproj") -AND
            !$file.Name.EndsWith(".sln") -AND
            !$file.Name.EndsWith(".plist") -AND
            !$file.Name.EndsWith(".appxmanifest") -AND
            !$file.Name.EndsWith(".config") -AND
            !$file.Name.EndsWith(".json"))
        {
            continue;
        }
        
        Write-Color "Modifying ", $file.Name -Color White, Yellow

         (Get-Content $file.PSPath) |
             Foreach-Object { $_ -replace "StarterKit", $project } |
             Set-Content $file.PSPath
    }

    Write-Host

    # RENAME FILES
    Write-Color "File Renames StarterKit => ", $project -Color Green, Green;
    Write-Color "-----------------------------------------------------" -Color Green;

    $files = Get-ChildItem $scriptDir * -rec |
        ?{ $_.FullName -inotmatch "\\bin\\" } |
        ?{ $_.FullName -inotmatch "\\obj\\" } |
        ?{ $_.FullName -inotmatch "\\packages\\" }
        
    foreach ($file in $files)
    {
        if ($file.Name.StartsWith("."))
        {
            continue;
        }
        
        if ($file -is [System.IO.DirectoryInfo])
        {
            continue;
        }

        $oldName = $file.Name;
        $newName = $file.Name.Replace("StarterKit", $project)
        
        if ($oldName -eq $newName)
        {
            continue;
        }
        
        Write-Color "Renaming ", $oldName, " => ", $newName -Color White, Yellow, White, Red
        
        Rename-Item -Path $file.FullName -NewName $newName
    }

    Write-Host

    #  RENAME DIRS
    Write-Color "Directory Renames StarterKit => ", $project -Color Green, Green;
    Write-Color "-----------------------------------------------------" -Color Green;

    Recursive-Directory-Rename $project $scriptDir
}

Write-Host