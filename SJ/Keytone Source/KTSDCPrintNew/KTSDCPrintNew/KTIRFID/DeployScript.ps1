param($solutionfilename,$projectdir,$outdir, $targetdir,$targetfilename,$solutiondir, $configName)

trap {

$errorTxt = $_.Exception.Message

write-output "$errorTxt"

break

}


write-output ""

write-output "*******************************************"

write-output "Required Args:"

write-output ""

write-output "Solution File Name: $solutionfilename"

write-output "projectdir : $projectdir"

write-output "outdir : $outdir"

write-output "targetdir : $targetdir"

write-output "Target File Name : $targetfilename"

write-output "Solution Directory: $solutiondir"

write-output "Configuration Name: $configname"

 

write-output "*******************************************"

write-output "Additional Args:"

write-output ""