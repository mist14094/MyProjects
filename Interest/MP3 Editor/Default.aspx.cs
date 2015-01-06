using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Id3;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = @"C:\Users\vramalingam\Downloads\Music\Lingaa";
        if (Directory.Exists(path))
        {
            int i = 0;
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
              //  using (var mp3 = new Mp3File(file))
              //  {
           //         if (i == 0)
                    {
                        try
                        {
                            //Id3Tag tag = mp3.GetTag(Id3TagFamily.FileStartTag);
                            //tag.Artists.Value = "";
                            //var fileName = file;
                            //tag.Title.Value = fileName;
                            //tag.Artists.Value = "";
                            //tag.Comments.Clear();
                            //mp3.WriteTag(tag, 1, 0, WriteConflictAction.Replace);

                            using (var fileStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                            {
                                using (var mp3Stream = new Mp3Stream(fileStream, Mp3Permissions.ReadWrite))
                                {
                                    var id3Tag = mp3Stream.GetAllTags()[0];
                                    var fileName = new FileInfo(file).Name;
                                    
                                    fileName = fileName.Substring(0, fileName.Length - 4);
//Change Only the Album Artists
                                    id3Tag.Title.Value = fileName;
                                    id3Tag.Artists.Values.Clear();
                         //           id3Tag.Artists.Value = "A.R.Rahman";
                                    id3Tag.Album.Value = "Lingaa";
                                    id3Tag.Composers.Value = "A.R.Rahman";
                                    id3Tag.Comments.Clear();
                                    mp3Stream.WriteTag(id3Tag, 1, 0, WriteConflictAction.Replace);
                                }
                                fileStream.Close();
                            }
                         
                        }
                        catch (Exception ex)
                        {

                        }
                        i++;
                //    }
                }
                    System.IO.File.Move(file, file.Replace(" - TamilTunes.com", ""));


            }
        }

    }
}