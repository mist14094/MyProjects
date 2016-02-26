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

echo "------------------TagConfig Start----------------------"
$dir = join-path $projectdir$outdir -childpath XSD 
echo "New Directory1: $dir" 
test-path $dir 
if($? -eq "False")
 { mkdir $dir; }
 
$TagConfig = join-path $projectdir$outdir -childpath XSD\TagConfig
echo "New Directory2: $TagConfig" 
test-path $TagConfig 
if($? -eq "False")
 { mkdir $TagConfig; }
 
 $dir = join-path $projectdir -childpath TagDataXForm\RFIDTagDataXFormSpec.xsd
 test-path $dir 
 if($? -eq "True")
 { 
   copy $dir -destination $TagConfig 
 }
 
 $dir = join-path $projectdir -childpath MetaDataEditor\RFIDTagDataTemplateSpec.xsd
 test-path $dir 
 if($? -eq "True")
 { 
   copy $dir -destination $TagConfig 
 }
 
 echo "------------------TagConfig End-----------------------------"
 
 echo "----------------XSD\ConfigParams Start-----------------------"
 
 $ConfigParams = join-path $projectdir$outdir -childpath XSD\ConfigParams
echo "New Directory3: $ConfigParams" 
test-path $ConfigParams 
if($? -eq "False")
 { mkdir $ConfigParams; }
 
 
 $dir = join-path $projectdir -childpath ConfigParams\XSD\ConfigParams\*.xsd
 test-path "$projectdir\ConfigParams\XSD\ConfigParams"
 if($? -eq "True")
 { 
   copy $dir -destination $ConfigParams
 }
 echo "-----------------XSD\ConfigParams END-------------------------"
 
 
 echo "----------------XSD\EPCFormatConfig Start----------------------"
 
  $EPCFormatConfig = join-path $projectdir$outdir -childpath XSD\EPCFormatConfig
echo "New Directory4: $EPCFormatConfig" 
test-path $EPCFormatConfig 
if($? -eq "False")
 { mkdir $EPCFormatConfig; }
 
 $dir = join-path $projectdir -childpath EPCTagEncoding\XmlTranslationTableSpec.xsd
 test-path $dir 
 if($? -eq "True")
 { 
   copy $dir -destination $EPCFormatConfig
 }
 
 echo "-----------------XSD\EPCFormatConfig End-------------------------"
 
 
 $Data = join-path $projectdir$outdir -childpath Data
echo "New Data: $Data" 
test-path $Data 
if($? -eq "False")
 { mkdir $Data; }
 
  $DConfig = join-path $projectdir$outdir -childpath Data\ConfigParams
echo "New DConfig: $DConfig" 
test-path $DConfig
if($? -eq "False")
 { mkdir $DConfig; }
 
 $GlobalConfigs = join-path $projectdir$outdir -childpath Data\ConfigParams\GlobalConfigs
echo "New GlobalConfigs: $GlobalConfigs" 
test-path $GlobalConfigs
if($? -eq "False")
 { mkdir $GlobalConfigs; }
 
 echo "PPDebug Start"
  $dir = join-path $targetdir -childpath Data\ConfigParams\GlobalConfigs\KTone.Global.config
  echo "New dir: $dir" 

   test-path $dir 
   if($? -eq "True")
   { 
       echo "file Exist deleting...."
       remove-item $dir
   }

   echo "AssetTrace Copying KTone.Global.config to Build.Framework folder"
   $dir = join-path $projectdir -childpath ConfigParams\Data\ConfigParams\GlobalConfigs\KTone.Global.config
   echo "Last Dir: $dir"
   copy $dir -destination "$GlobalConfigs\KTone.Global.config"
  
echo "PPDebug END: $GlobalConfigs\KTone.Global.config"
 
