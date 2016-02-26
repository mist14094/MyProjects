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
 
#$product = get-childitem env:product
#echo "Environment Var" $product
 
  #if ($product.Value -eq "AssetTrace")
	echo "Copying AT_KTWPFAppBase.dll.config"
   $dir = join-path $projectdir -childpath AT_KTWPFAppBase.dll.config
   copy $dir -destination $targetdir
   $newname = join-path $targetdir -childpath AT_KTWPFAppBase.dll.config
   $check = join-path $targetdir -childpath KTWPFAppBase.dll.config
   echo "checking directory: $check  "
	if(test-path $check )
	{
		Remove-Item $check
	}
   rename-item -path $newname -newname KTWPFAppBase.dll.config
 
 #}
  
$source = join-path $projectdir -childpath Images\*.*
$Images = join-path $targetdir -childpath Images
echo "New Directory1: $Images" 
test-path $Images 
if($? -eq "False")
{  mkdir $Images ;
   copy $source -destination $images
}

