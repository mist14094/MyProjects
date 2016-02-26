param($solutionfilename,$projectdir,$outdir, $targetdir,$targetfilename,$solutiondir, $configName)

trap {

$errorTxt = $_.Exception.Message

echo "Error Found :$errorTxt"

break

}

echo ""

echo "*******************************************"

echo "Required Args:"

echo ""

echo "Solution File Name: $solutionfilename"

echo "projectdir : $projectdir"

echo "outdir : $outdir"

echo "targetdir : $targetdir"

echo "Target File Name : $targetfilename"

echo "Solution Directory: $solutiondir"

echo "Configuration Name: $configname"

 
echo "*******************************************"

echo "Additional Args:"

echo ""

echo "***************PostBuild Start************************"
 
  
$source = join-path $projectdir -childpath Images\*.*
$Images = join-path $targetdir -childpath Images
echo "New Directory1: $Images" 
test-path $Images 
if($? -eq "False")
{  mkdir $Images ;
   copy $source -destination $images
}

$source = join-path $projectdir -childpath ..\..\..\..\Build.Framework\KTClients\KTWPFAppBase\KTWPFAppBase.dll.config
copy $source -destination $targetdir

$source = join-path $projectdir -childpath ..\..\..\..\Build.Framework\KTClients\KTWPFAppBase\Images\*.*
copy $source -destination $targetdir\Images


