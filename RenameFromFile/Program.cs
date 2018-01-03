using System;
using System.IO;
using System.Linq;



namespace RenameFromFile
{
    class Program
    {

        // Improvement ideas:
        // If the rename fails, write why (Try 1 of fixing it, no promises made)
        // If rename.csv is found in the directory, rename automatically via doubleclick (should be done)


        static void Main(string[] args)
        {
            // Take one line from a txt and rename the current file to the current line in the txt file.
            // https://docs.esellercloud.com/tutorials/products/bulk-uploading-product-images.aspx
            // dataoriginid_productid_variantid_sortorder_filename.extension
            // Where _ is a delimiter
            // String _ string _ string _ Number _ string _ .Extention
            // 

            
            Program p = new Program();
            if (args.Count() == 0)
            {
                if(File.Exists("Rename.csv"))
                {
                    p.Rename("Rename.csv");
                }
                Console.WriteLine("Write filename that has the old and new names after the program name");
                
            }
            else if (args.Count() ==1)
            {
                string filepath = args[0];

                p.Rename(filepath);

            }

        }

        public void Rename(string filepath)
        {
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
                if (File.Exists(f.oldFile) && f.oldFile != "")
                {
                    if (File.Exists(f.newFile))
                    {
                        Console.WriteLine(f.newFile + " exists");
                    }
                    else
                    {
                        File.Copy(f.oldFile, f.newFile);
                        Console.WriteLine(f.newFile + " created");
                    }

                }
                else
                {
                    Console.WriteLine(f.oldFile + " does not exist");
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
