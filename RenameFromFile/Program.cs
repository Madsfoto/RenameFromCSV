using System;
using System.IO;
using System.Linq;



namespace RenameFromFile
{
    class Program
    {



        static void Main(string[] args)
        {
            // Take one line from a txt and rename the current file to the current line in the txt file.
            // https://docs.esellercloud.com/tutorials/products/bulk-uploading-product-images.aspx
            // dataoriginid_productid_variantid_sortorder_filename.extension
            // Where _ is a delimiter
            // String _ string _ string _ Number _ string _ .Extention
            // 

            /* Examples
             * _2343__1_large-flashlight.jpg 
            An image, large-flashlight.jpg, is attached to the product with ID 2343 

            _43_2_1_grey-hat.jpg 
            An image, grey-hat.jpg, is attached to the variant with ID 2 of the product with ID 43 

            _43_3_1_black-hat.jpg 
            An image, black-hat.jpg, is attached to the variant with ID 3 of the product with ID 43 

            _43_3_2_black-hat-closeup.jpg 
            An additional image, black-hat-closeup.jpg, is attached to the variant with ID 3 of the product with ID 43. The image is shown with sort order 2.
            */

            Program p = new Program();
            if (args.Count() == 0)
            {
                Console.WriteLine("Write filename that has the old and new names after the program name");
            }
            else if (args.Count() ==1)
            {

                string filepath = args[0];

                var lines = File.ReadAllLines(filepath);

                var data = from l in lines.Skip(1)
                           let split = l.Split(';')
                           
                           select new OldNew
                           {
                               
                               oldFile = split[0],
                               newFile = split[1],
                           };
                
                
                foreach (var f in data)
                {
                    
                    if (File.Exists(f.oldFile) && f.oldFile!="")
                    {
                        if (File.Exists(f.newFile))
                        {

                        }
                        else
                        {
                            File.Copy(f.oldFile, f.newFile);
                        }
                        
                    }

                }


            }
            else if(args.Count()==2)
            {
                string filepath = args[0];

                var lines = File.ReadAllLines(filepath);

                var data = from l in lines.Skip(1)
                           let split = l.Split(';')
                           select new OldNew
                           {
                               oldFile = split[0],
                               newFile = split[1],
                           };

                foreach (var f in data)
                {
                    if (File.Exists(f.oldFile))
                    {
                        File.Move(f.oldFile, f.newFile);
                    }
                }

            }















        }
    }

    internal class OldNew
    {
        public string oldFile { get; set; }
        public string newFile { get; set; }
    }
}
