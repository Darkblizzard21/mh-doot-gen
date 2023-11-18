# URL of the web page containing tables and headers
$url = "https://github.com/mhvuze/MonsterHunterRiseModding/wiki/Weapon-IDs-(LS,SA,GL,DB,HH)"

# ID of the header element preceding the table
$headerID = "user-content-hunting-horn"

# File path to save the HTML content
$filePath = ".\tmp\page_content.html"

# Download the web page
$response = Invoke-WebRequest -Uri $url

# Check if the request was successful (status code 200)
if ($response.StatusCode -eq 200) {
    # Parse HTML content to find the header by its ID
    $htmlContent = $response.Content
    $htmlDocument = New-Object -ComObject "HTMLFile"
    $htmlDocument.IHTMLDocument2_write($htmlContent)

    # Find the header by its ID
    $header = $htmlDocument.getElementById($headerID)

    # Check if the header was found
    if ($header -ne $null) {
        # Get the next sibling element
        $nextElement = $header.nextSibling

        # Check if the next element is a table
        if ($nextElement -ne $null -and $nextElement.tagName -eq "TABLE") {
            # Save the entire HTML content to a file
            $htmlContent | Out-File -FilePath $filePath

            Write-Output "HTML content saved to $($filePath)"
        } else {
            Write-Output "The next element after the header is not a table."
            $htmlContent | Out-File -FilePath $filePath
        }
    } else {
        Write-Output "Header element with the specified ID not found."
    }
} else {
    Write-Output "Failed to retrieve the web page. Status code: $($response.StatusCode)"
}