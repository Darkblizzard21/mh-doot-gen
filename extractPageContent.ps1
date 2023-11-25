# File path of the downloaded HTML file
$filePath = ".\tmp\page_content.html"
$outPath = ".\doot-gen\HornData.cs"
# Content to search for within an element
$searchContent = "Hrn018"

# Check if the file exists
if (Test-Path $filePath) {
    # Load the HTML content from the file
    $htmlContent = Get-Content -Path $filePath -Raw

    # Parse HTML content
    $htmlDocument = New-Object -ComObject "HTMLFile"
    $htmlDocument.IHTMLDocument2_write($htmlContent)

    # Find an element with specific content
    $foundElement = $htmlDocument.getElementsByTagName("td") | Where-Object { $_.innerText -eq $searchContent }  # Replace "element_tag" with your element tag

    # Check if the element with the specified content was found
    if ($foundElement -ne $null) {
        # Find the parent table of the found element
        $parentTable = $foundElement.parentElement
        while ($parentTable -ne $null -and $parentTable.tagName -ne "TABLE") {
            $parentTable = $parentTable.parentElement
        }

        # Print all elements within the parent table
        if ($parentTable -ne $null -and $parentTable.tagName -eq "TABLE") {
            $groupedValues = @{}

            # Get all table rows within the parent table
            $rows = $parentTable.getElementsByTagName("tr")

            # Process each row in the table
            foreach ($row in $rows) {
                # Get all cells within the row
                $cells = $row.getElementsByTagName("td")
                
                # Check if the row has enough cells
                $fourthColumnValue = $cells.Item(3).innerText  # 4th column (index 3)
                $sixthColumnValue = $cells.Item(5).innerText  # 6th column (index 5)

                # Group values by the 4th column value
                if ($groupedValues.ContainsKey($fourthColumnValue)) {
                    if ($groupedValues[$fourthColumnValue] -notlike "*$sixthColumnValue*") {
                        $groupedValues[$fourthColumnValue] += ", $sixthColumnValue"
                    }
                } else {
                    $groupedValues[$fourthColumnValue] = $sixthColumnValue
                }
                
            }
            $sortedKeys = $groupedValues.Keys | sort 

            $outfile = "//Automaticly generated file"
            $outfile += "using System.Collections;`nusing System.Collections.Generic;`n`n"
            $outfile += "namespace doot_gen`n`{`n"
            $outfile += "`tenum Horns`n`t`{`n"

            foreach ($key in $sortedKeys) {
                $nr = $key.Substring(3)
                $outfile += "`t`t$key = $nr,`n "
            }
            $outfile += "`t`}`n`n"

            
            $outfile += "`tstatic class HornExtensions`n`t`{`n"

            
		    $outfile += "`t`tpublic static readonly List<string> hornModelIds = new() { "
            foreach ($key in $sortedKeys) {
                $outfile += "`"$key`","
            }
            $outfile += "};`n`n"

            $outfile += "`t`tpublic static readonly List<Horns> allHorns = new() { "
            foreach ($key in $sortedKeys) {
                $outfile += "`Horns.$key`,"
            }
            $outfile += "};`n`n"

            $outfile += "`t`tpublic static IEnumerable<string> PossibleHornFiles(this Horns horn)`n"
            $outfile += "`t`t{`n"
            $outfile += "`t`t`tstring hornNumber = ((int)horn).ToString(`"D3`");`n"
            $outfile += "`t`t`tyield return `"pl_wp_hrn_song_`" + hornNumber + `"_media.bnk.2.X64`";`n"
            $outfile += "`t`t`tyield return `"pl_wp_hrn_song_`" + hornNumber + `"_khk_media.bnk.2.X64`";`n"
            $outfile += "`t`t`tyield return `"pl_wp_hrn_gimmick_`" + hornNumber + `"_media.bnk.2.X64`";`n"
            $outfile += "`t`t`tyield return `"pl_wp_hrn_gimmick_`" + hornNumber + `"_khk_media.bnk.2.X64`";`n"
°           $outfile += "`t`t`tyield return `"pl_wp_hrn_oneoff_`" + hornNumber + `"_media.bnk.2.X64`";`n"
            $outfile += "`t`t`tyield return `"pl_wp_hrn_oneoff_`" + hornNumber + `"_khk_media.bnk.2.X64`";`n"
            $outfile += "`t`t}`n`n"

            $outfile += "`t`tpublic static IEnumerable<string> ExistingHornFiles(this Horns horn, string soundPath)`n"
            $outfile += "`t`t{`n"
            $outfile += "`t`t`treturn PossibleHornFiles(horn).Select(file => soundPath + file).Where(file => File.Exists(file));`n"
            $outfile += "`t`t}`n`n"

            $outfile += "`t`tpublic static string GetHornModelId(this Horns horn)`n`t`t`{`n"
            $outfile += "`t`t`tswitch (horn)`n`t`t`t`{`n"
            foreach ($key in $sortedKeys) {
                $outfile += "`t`t`t`tcase Horns.$key`:`n"
                $outfile += "`t`t`t`t`treturn `"$key`";`n"
            }
            $outfile += "`t`t`t`}`n"
            $outfile += "`t`t`treturn `"N/A`";`n"
            $outfile += "`t`t`}`n`n"
            
            $outfile += "`t`tpublic static string  GetHornName(this Horns horn)`n`t`t`{`n"
            $outfile += "`t`t`tswitch (horn)`n`t`t`t`{`n"
            foreach ($key in $sortedKeys) {
                $outfile += "`t`t`t`tcase Horns.$key`:`n"
                $outfile += "`t`t`t`t`treturn `"$($groupedValues[$key])`";`n"
            }
            $outfile += "`t`t`t`}`n"
            $outfile += "`t`t`treturn `"N/A`";`n"
            $outfile += "`t`t`}`n`n"
            
            
            $outfile += "`t`}`n"
            $outfile += "`}`n"
            Write-Output "$outfile"
            $outfile | Out-File -FilePath $outPath
            (Get-Content $outPath) | Out-File $outPath
        } else {
            Write-Output "No parent table found for the element with content '$searchContent'."
        }
    } else {
        Write-Output "Element with content '$searchContent' not found."
    }
} else {
    Write-Output "HTML file not found at path: $($filePath)"
}